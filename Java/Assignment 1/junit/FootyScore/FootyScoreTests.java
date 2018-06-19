package junit.FootyScore;

import static org.junit.Assert.*;

import java.util.Random;

import org.junit.Before;
import org.junit.Test;

public class FootyScoreTests {

    private static Random random = new Random();
    
    // Test 0: Declare object
    FootyScore teamOne;
    
    // Clear the footyScore object before every test.
    @Before
    public void setUpFootyScore() {
        teamOne = new FootyScore();
    }
    
    // Test 1: score calc no scoring == 0 score
    @Test
    public void testZero() {
        assertEquals(teamOne.getPoints(), 0);
    }
    
    // Test 2: score calc random kicks = # * 6
    @Test
    public void testGoals() {
        int goals = random.nextInt(10);
        for (int i = 0; i < goals; i++) {
            teamOne.kickGoal();
        }
        assertEquals(teamOne.getPoints(), goals * 6);
    }
    
    // Test 3: score calc random behinds = # * 1
    @Test
    public void testBehinds() {
        int behinds = random.nextInt(10);
        for (int i = 0; i < behinds; i++) {
            teamOne.kickBehind();
        }
        assertEquals(teamOne.getPoints(), behinds);
    }
    
    // Test 4: sayScore zero scores
    @Test
    public void testSayScoreZero() {
        assertEquals(teamOne.sayScore(), "0, 0, 0");
    }
    
    // Test 5: sayScore with random of each of goals and behinds
    @Test
    public void testSayScoreRandoms() {
        int behinds = random.nextInt(10);
        for (int i = 0; i < behinds; i++) {
            teamOne.kickBehind();
        }
        int goals = random.nextInt(10);
        for (int i = 0; i < goals; i++) {
            teamOne.kickGoal();
        }
        String result = Integer.toString(goals) + ", " + Integer.toString(behinds) + ", " + Integer.toString(goals * 6 + behinds); 
        assertEquals(teamOne.sayScore(), result);
    }
    
    //Test 6: inFrontOf self should always equal false
    @Test
    public void testInFrontOfSelf() {
        assertEquals(teamOne.inFrontOf(teamOne), false);
    }
    
    // Test 6: inFrontOf other team with lower score
    @Test
    public void testInFrontOfLower() {
        FootyScore teamTwo = new FootyScore();
        teamOne.kickGoal();
        assertEquals(teamOne.inFrontOf(teamTwo), true);
    }
    
    // Test 7: inFrontOf other team with higher score
    @Test
    public void testInFrontOfHigher() {
        FootyScore teamTwo = new FootyScore();
        teamTwo.kickGoal();
        assertEquals(teamOne.inFrontOf(teamTwo), false);
    }

}













