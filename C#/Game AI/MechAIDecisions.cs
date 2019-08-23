using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechAIDecisions : MonoBehaviour {

    //Links to Mech AI Systems
    MechSystems mechSystem;
    MechAIMovement mechAIMovement;
    MechAIAiming mechAIAiming;
    MechAIWeapons mechAIWeapons;

    ////FSM State Implementation
    //public enum MechStates {
    //    Roam,
    //    Attack,
    //    Pursue,
    //    Flee
    //}
    //public MechStates mechState;

    //Roam Variables
    public GameObject[] patrolPoints;
    private int patrolIndex = 0;
    public GameObject[] aimTargets;

    // Humanising Variables
    private GameManager gameManager;
    private float reactionTime;
    [SerializeField] private float fun = 0.5f;
    [SerializeField] private float aggression = 0.5f;
    [SerializeField] private float status;
    private float lastEngagement;
    private int streak;
    private int streakStart;
    private float kdrLast;
    private float scoreCheckTimer;
    private float scoreCheckDelay = 45.0f; // Time in seconds to check 
    private float thresh_high = 0.75f;
    private float thresh_low = 0.4f;
    private int thresh_status = 1500;
    private float lookTimer;
    private bool hasLooked = true;
    private float lookDelay = 0.25f;
    private GameObject lastLook;
    private float strafeTimer;
    private bool strafing;
    private GameObject strafeTarget;

    // Targeting Variables
    private GameObject[] seenTargets = new GameObject[0];
    private GameObject closestPickup;
    private GameObject vulnerableTarget;
    private List<GameObject> aggressorTargets = new List<GameObject>();
    private List<float> aggressorLossTimers = new List<float>();
    private float aggressorLossDelay = 3.0f;

    //Attack Variables
    private GameObject attackTarget;
    private float attackTime = 3.5f;
    private float attackTimer;

    //Pursue Variables
    public GameObject pursueTarget;
    private Vector3 pursuePoint;
    private Vector3 pursuePointLast;

    //Flee Variables
    private Vector2 fleeFrom;
    private GameObject fleeTo;

    // Use this for initialization
    void Start() {
        //Collect Mech and AI Systems
        mechSystem = GetComponent<MechSystems>();
        mechAIMovement = GetComponent<MechAIMovement>();
        mechAIAiming = GetComponent<MechAIAiming>();
        mechAIWeapons = GetComponent<MechAIWeapons>();

        //Roam State Startup Declarations
        patrolPoints = GameObject.FindGameObjectsWithTag("Patrol Point");
        patrolIndex = Random.Range(0, patrolPoints.Length - 1);
        mechAIMovement.Movement(patrolPoints[patrolIndex].transform.position, 1);

        // Initialise with a random reaction time, and set first score check timer
        reactionTime = Random.Range(0.22f, 0.255f);

        // Get a reference to the gamemanager, for checking score
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager) {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Set this current life's starting score for calculating streak, then update the mind
        streakStart = gameManager.playerScores[mechSystem.ID];
        UpdateMind();
    }

    // Update is called once per frame
    void Update() {
        AcquireTarget();
    }

    #region TreeBooleans

    // When being attacked by two or more enemies
    [Task] bool MultipleAggressors() {
        return aggressorTargets.Count > 1;
    }

    // If the current target is attacking this mech
    [Task] bool TargetSeesMe() {

        return aggressorTargets.Contains(attackTarget);
    }

    [Task] bool BeingTargeted() {

        return aggressorTargets.Count > 0;
    }

    // If the bot is "having fun"
    [Task] bool Fun() {
        return fun >= 0.0f;
    }

    // If the mech has a target to attack
    [Task] bool HasAttackTarget() {

        if (attackTarget) {
            return true;
        }
        return false;
    }

    // If the mech has line of sight to the attack target
    [Task] bool TargetLOS() {

        if (mechAIAiming.LineOfSight(attackTarget)) {
            return true;
        }
        return false;
    }

    //Method for checking heuristic status of Mech to determine if Fleeing is necessary
    [Task]
    private bool StatusCheck() {

        status = mechSystem.health + mechSystem.energy + (mechSystem.shells * 7) + (mechSystem.missiles * 10);

        // If bot ammo and health is high enough, based upon aggression
        if (status > thresh_status * (1.0f - aggression / 2))
        {
            return false;
        }

        return true;
    }

    // If there is enough energy, and the enemy is within the cone of fire
    [Task] bool CheckLasers() {

        return mechSystem.energy > 10 && mechAIAiming.FireAngle(20);
    }

    // If there is enough cannon ammo, and the enemy is within the cone of fire
    [Task] bool CheckCannons() {

        return Vector3.Distance(transform.position, attackTarget.transform.position) > 25
            && mechSystem.shells > 4 && mechAIAiming.FireAngle(15);
    }

    // If there is an enemy in range of the laser, and there's enough energy
    [Task] bool CheckBeam() {

        return Vector3.Distance(transform.position, attackTarget.transform.position) > 20
            && Vector3.Distance(transform.position, attackTarget.transform.position) < 50
            && mechSystem.energy >= 300 && mechAIAiming.FireAngle(10);
    }

    // If there is enough missiles, and the targeted enemy is far enough away
    [Task] bool CheckMissiles() {

        return Vector3.Distance(transform.position, attackTarget.transform.position) > 50
            && mechSystem.missiles >= 18 && mechAIAiming.FireAngle(5);
    }

    // If this mech can see two or more enemies
    [Task] bool IsEnemyEngagement() {

        if (seenTargets.Length > 1) {

            return true;
        }
        return false;
    }

    [Task] bool PickupWorthwhile() {

        // If any resource is below half, or health is below 75%
        return  mechSystem.health < 2000 * 0.75f || 
                mechSystem.energy < 750 * 0.5f || 
                mechSystem.shells < 30 * 0.5f || 
                mechSystem.missiles < 54 * 0.5f;
    }

    [Task] bool EnemyTooClose() {

        if (seenTargets.Length > 0) {
            // If ANY seen enemy is within 5.0f
            foreach (GameObject target in seenTargets) {

                if (target != null) {
                    if (Vector3.Distance(transform.position, target.transform.position) < 5.0f) {
                        return true;
                    }
                }
            }
        }
        if (aggressorTargets.Count > 0) {
            // If ANY aggressive enemy is within 5.0f
            foreach (GameObject target in aggressorTargets) {

                if (target != null) {
                    if (Vector3.Distance(transform.position, target.transform.position) < 5.0f) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    #endregion

    // Recalculate the current fun and aggression values
    [Task] private bool UpdateMind() {

        // Get number of kills this life
        streak = gameManager.playerScores[mechSystem.ID] - streakStart;

        if (Time.time > scoreCheckTimer) {

            int score = gameManager.playerScores[mechSystem.ID];
            int deaths = gameManager.playerDeaths[mechSystem.ID];

            if (deaths > 0) {
                // Update kill-death ratio
                kdrLast = (score - deaths) / deaths;

            } else if (score < deaths){

                // Adjust kdr to be negative for low kdr
                kdrLast = -(deaths - score) / deaths;

            } else {
                // Set it to not affect fun/aggression calculations
                kdrLast = 0.0f;
            }

            // Set the next check time
            scoreCheckTimer += scoreCheckDelay;
        }

        // Modify fun by streak, kdr, and last 
        fun = Mathf.Clamp(kdrLast + 0.25f * streak + (1.0f - 0.01f * (Time.time - lastEngagement)), -1.0f, 1.0f);

        // Modify aggression by streak, fun, and number of aggressors
        aggression = Mathf.Clamp(0.5f + fun / 2 + 0.25f * streak - aggressorTargets.Count * 0.25f, 0.01f, 1.0f);

        return false;
    }


    [Task]
    private bool AcquireTarget() {

        // Stop strafing when target changes or is destroyed
        if (attackTarget != null) {
            if (attackTarget != strafeTarget) {
                strafing = false;
            }
        } else {
            strafing = false;
        }

        //Acquire valid attack target: perform frustum and LOS checks and determine closest target
        mechAIAiming.FrustumCheck();

        // Get a reference to all targets within attack range
        seenTargets = mechAIAiming.currentTargets.ToArray();

        if (seenTargets.Length > 0) {

            foreach (GameObject target in seenTargets) {

                // If a seen target is currently shooting this mech, attack that target
                if (aggressorTargets.Contains(target)) {

                    attackTarget = target;
                }
            }
        }

        // If there's seen targets and a target very close, target the closest seen
        if (mechAIAiming.currentTargets.Count > 0){

            try {
                if (Vector3.Distance(transform.position,
                mechAIAiming.ClosestTarget(
                    mechAIAiming.currentTargets)
                    .transform.position) < 10.0f)
                {

                    mechAIWeapons.laserBeamAI = false;  //Hard disable on laserBeam

                    attackTarget = mechAIAiming.ClosestTarget(mechAIAiming.currentTargets);

                    return true;
                }
            } catch {
                return false;
            }
        }

        // If there's still no target or a target very close, target the closest seen
        if (!attackTarget) {

            mechAIWeapons.laserBeamAI = false;  //Hard disable on laserBeam

            attackTarget = mechAIAiming.ClosestTarget(mechAIAiming.currentTargets);

            return true;
        }

        // Clear aggressors if their timer has elapsed
        if (aggressorTargets.Count > 0) {

            // From last to first aggressor
            for (int index = aggressorTargets.Count - 1; index >= 0; index--) {

                if (Time.time > aggressorLossTimers[index]) {

                    // Remove that aggressor and it's timer
                    aggressorTargets.RemoveAt(index);
                    aggressorLossTimers.RemoveAt(index);
                }
            }
        }

        return false;
    }

    //FSM Behaviour: Roam - Roam between random patrol points
    [Task] private bool Roam() {
        //Move towards random patrol point
        if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].transform.position) <= 2.0f) {
            patrolIndex = Random.Range(0, patrolPoints.Length - 1);
        } else {
            mechAIMovement.Movement(patrolPoints[patrolIndex].transform.position, 1);
        }
        //Look at random patrol points
        mechAIAiming.RandomAimTarget(patrolPoints);

        return true;
    }

    // Attack
    [Task] private void Attack() {

        //Child object correction - wonky pivot point
        mechAIAiming.aimTarget = attackTarget.transform.GetChild(0).gameObject;

        //Track position of current target to pursue if lost
        pursuePoint = attackTarget.transform.position;

        // Update the last engagement time
        lastEngagement = Time.time;
    }

    [Task] private void AttackMove() {

        //Move Towards attack Target
        if (Vector3.Distance(transform.position, attackTarget.transform.position) >= 45.0f && !strafing)
        {
            mechAIMovement.Movement(attackTarget.transform.position, 45);
        }
        //Otherwise "strafe" - move towards random patrol points at intervals
        else if (Time.time > attackTimer)
        {
            // 50% chance left or right
            if ( strafeTimer < Time.time) {

                float coin = Random.Range(0, 1);

                Vector3 strafeDir = attackTarget.transform.position - transform.position;

                // Calculate perpendicular
                strafeDir = new Vector3(strafeDir.z, strafeDir.y, -strafeDir.x);

                // Half the time invert from clockwise perpendicular to counterclockwise
                if (coin >= 0.5f) {
                    strafeDir = -strafeDir;

                    // Don't flip Y value
                    strafeDir.y = -strafeDir.y;
                }

                // Update the timer, and strafing
                strafeTimer = Time.time + Random.Range(0.1f, 1.0f);
                strafing = true;
                strafeTarget = attackTarget;

                Debug.DrawLine(transform.position, strafeDir, Color.green);
                mechAIMovement.Movement(strafeDir, 1);
            }
        }
    }

    //FSM Behaviour: Pursue
    [Task] void Pursue() {

        //Move towards last known position of attackTarget
        if (Vector3.Distance(transform.position, pursuePoint) > 3.0f) {
            mechAIMovement.Movement(pursuePoint, 1);
            mechAIAiming.RandomAimTarget(patrolPoints);

            // Turn off strafing
            strafing = false;
        }
        //Otherwise if reached and have not re-engaged, reset attackTarget and Roam
        else {
            attackTarget = null;
            patrolIndex = Random.Range(0, patrolPoints.Length - 1);
            mechAIMovement.Movement(patrolPoints[patrolIndex].transform.position, 1);
        }
    }

    //FSM Behaviour: Flee
    [Task] void Flee() {

        // If there is no flee point yet, get one
        if (!fleeTo) {
            GetBestFleePoint();

        // Flee towards point closest to 110 degrees horizontally away from enemy(s)
        } else if (Vector3.Distance(transform.position, fleeTo.transform.position) <= 2.0f) {

            GetBestFleePoint();

        } else {

            Debug.DrawLine(fleeTo.transform.position, fleeTo.transform.position + Vector3.up * 10, Color.cyan);
            mechAIMovement.Movement(fleeTo.transform.position, 1);
        }

        // If there is an attack target, set it as the last look Target 
        if (attackTarget && 
            Time.time > lookTimer && 
            hasLooked) {

            lastLook = attackTarget.transform.GetChild(0).gameObject;

            // Start looking at the flee point, and set up a timer
            mechAIAiming.aimTarget = fleeTo;
            lookTimer = Time.time + lookDelay;
            hasLooked = false;

        } else if  (attackTarget && 
                    Time.time > lookTimer && 
                    !hasLooked) {

            // Start looking at the flee point, and set up a timer
            mechAIAiming.aimTarget = lastLook;
            hasLooked = true;
            lookTimer = Time.time + lookDelay * 10;
            lastLook = null;

        } else if ( lastLook == null && 
            attackTarget == null) {

            //Look at random patrol points
            mechAIAiming.RandomAimTarget(patrolPoints);
        }
    }

    private bool GetBestFleePoint() {

        Vector2 point = Vector2.zero;
        Vector2 botPoint = new Vector2(transform.GetChild(0).position.x, transform.GetChild(0).position.z);

        if (MultipleAggressors())
        {

            foreach (GameObject aggressor in aggressorTargets)
            {
                if (aggressor != null) {
                    // Add their corrected positions to the point
                    point.x += aggressor.transform.GetChild(0).position.x;
                    point.y += aggressor.transform.GetChild(0).position.z;
                }
            }

            // Average their positions
            point /= aggressorTargets.Count;

        } else {

            if (attackTarget != null) {
                // Find the target's point
                point.x = attackTarget.transform.GetChild(0).position.x;
                point.y = attackTarget.transform.GetChild(0).position.z;
            } else {
                return false;
            }
        }

        // Calculate a vector from the mech to the enemy point
        Vector2 enemyVec = point - botPoint;

        // Initialise containers for best point information
        GameObject bestPoint = attackTarget;
        float bestAngle = float.MaxValue;

        // Find the patrol point closest to 100 degress
        foreach (GameObject ppoint in patrolPoints)
        {

            // Calculate a vector to the current point being evaluated
            Vector2 fleeVec = new Vector2(ppoint.transform.position.x, ppoint.transform.position.z) - botPoint;

            float currentAngle = Vector2.Angle(enemyVec, fleeVec);

            // If this angle is closer to 110 degrees than the current best and not right nearby, replace it
            if (currentAngle > 110.0f &&
                currentAngle < bestAngle &&
                fleeVec.magnitude > 2.0f)
            {

                bestAngle = currentAngle;
                bestPoint = ppoint;
            }
        }

        // Set the flee point as the best point
        fleeTo = bestPoint;

        // If no best point was found, pick a random point to flee to
        if (bestPoint == attackTarget)
        {
            fleeTo = patrolPoints[Random.Range(0, patrolPoints.Length - 1)];
        }
        return true;
    }

    [Task] private bool GuardPickup() {

        float guardDist = 3.0f;
        FindPickup();

        if (closestPickup != null) {

            // If the mech is far from that pickup, move towards it
            if (Vector3.Distance(transform.position, closestPickup.transform.position) > guardDist) {

                Debug.DrawLine(transform.position, closestPickup.transform.position, Color.yellow);
                mechAIMovement.Movement(closestPickup.transform.position, guardDist);
            }

            // Return true if there is a pickup to move to
            return true;
        }

        return false;
    }

    [Task] private bool TakePickup() {

        FindPickup();

        if (closestPickup != null) {

            // Move to and pick it up
            mechAIMovement.Movement(closestPickup.transform.position, 0);
            Debug.DrawLine(transform.position, closestPickup.transform.position, Color.Lerp(Color.yellow, Color.red, 0.5f));

            return true;
        }
        return false;
    }

    private bool FindPickup() {

        var pickups = FindObjectsOfType<Pickup>();
        closestPickup = null;
        float closestDistance = float.MaxValue;

        foreach (Pickup pickup in pickups) {

            // If the current pickup can be seen and is closer, note it
            if (mechAIAiming.LineOfSight(pickup.gameObject) && 
                Vector3.Distance(transform.position, pickup.transform.position) < closestDistance) {

                closestPickup = pickup.gameObject;
            }
        }

        if (closestPickup != null) {
            return true;
        }
        return false;
    }

    //Method allowing AI Mech to acquire target after taking damage from enemy
    public void TakingFire(int origin) {

        //If not own damage and no current attack target, find attack target
        if (origin != mechSystem.ID && !attackTarget) {
            foreach (GameObject target in mechAIAiming.targets) {
                if (target) {
                    if (origin == target.GetComponent<MechSystems>().ID) {

                        // Instead of immediately targeting aggressor, take note of aggressor
                        if (!aggressorTargets.Contains(target)) {

                            aggressorTargets.Add(target);
                            aggressorLossTimers.Add(Time.time + aggressorLossDelay);

                        // If this aggressor is known about already, update the tracking loss timer
                        } else if (aggressorTargets.Contains(target)) {

                            aggressorLossTimers[aggressorTargets.IndexOf(target)] = Time.time + aggressorLossDelay;
                        }

                        // If there's no target currently, target the aggressor
                        if (!attackTarget) {
                            attackTarget = aggressorTargets[0];
                        }

                        // Update the last engagement time
                        lastEngagement = Time.time;
                    }
                }
            }
        }
    }

    #region Weapons

    [Task] void Lasers() {
        mechAIWeapons.Lasers();
    }

    [Task] void Cannons() {
        mechAIWeapons.Cannons();
    }

    [Task] void Beam() {
        mechAIWeapons.laserBeamAI = true;
    }
    [Task] bool BeamOff() {
        mechAIWeapons.laserBeamAI = false;
        return true;
    }

    [Task] void Missiles() {
        mechAIWeapons.MissileArray();
    }

    #endregion


    //Method controlling logic of firing of weapons: Consider minimum ammunition, appropriate range, firing angle etc
    [Task]
    private void AllWeapons() {

        Lasers();
        Cannons();
        Beam();
        Missiles();
    }

}
