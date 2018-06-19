package coll.EnrollmentManager;

import java.util.*;
import java.util.Map.Entry;

public class EnrollmentManager {
	
	private static HashMap<String, Set<String>> enrollments = new HashMap<String, Set<String>>();

	/**
	 * Enrolls a student into a unit.
	 * 
	 * @param unit
	 * @param student
	 */
	public static void enroll(String unit, String student) {
	    Set<String> data = new HashSet<String>();
        	    
	    if (enrollments.containsKey(unit)) {
		    data = enrollments.get(unit);
		    data.add(student);
		    enrollments.replace(unit, data);
		} else {
		    data.add(student);
		    enrollments.put(unit, data);
		}
	}

	/**
	 * Gets the HashMap containing the current enrollments.
	 * 
	 * @return
	 */
	public static HashMap<String, Set<String>> getEnrollments() {
		return enrollments;
	}

	/**
	 * Removes all enrollments form the HashMap.
	 */
	public static void wipeEnrollments() {
		enrollments = new HashMap<String, Set<String>>();
	}

	/**
	 * Withdraws a student from a unit.
	 * 
	 * @param unit
	 * @param student
	 */
	public static void withdrawEnrollment(String unit, String student) {
	    Set<String> data = new HashSet<String>();
	    
	    // If that student is in the database
	    if (enrollments.containsKey(unit)) {
	        // Get their data
	        data = enrollments.get(unit);
	        // If you can remove the specified unit
	        if (data.remove(student)) {
	            // Update the database without that unit
	            enrollments.replace(unit, data);
	        }
		}
	}

	/**
	 * Withdraws a student from all units they are enrolled in.
	 * 
	 * @param student
	 */
	public static void withdrawStudent(String student) {
	    Set<String> data = new HashSet<String>();
        
	    // For each entry in enrollments
        for (Entry<String, Set<String>> entry : enrollments.entrySet()) {
            // If it contains the specified student
            if (entry.getValue().contains(student)) {
                // Clone that data, remove the student, then replace the original data
                data = entry.getValue();
                data.remove(student);
                enrollments.replace(entry.getKey(), data);
            }
        }
	}

	/**
	 * Gets a list of all students of a particular discipline. E.g. If discipline is
	 * "ABC" then return a collection of all students enrolled in units that start
	 * with "ABC", so ABC301, ABC299, ABC741 etc. This method is non-trivial so it
	 * would help to first implement the helper method matchesDiscipline (below).
	 * 
	 * @param discipline
	 * @return
	 */
	public static Set<String> getStudents(String discipline) {
		
		// For each entry in enrollments
		for (Entry<String, Set<String>> entry : enrollments.entrySet()) {
            // If it contains the specified discipline
		    if (entry.getKey().contains(discipline)) {
                // Add it's key to the enrolled students set
		        return entry.getValue();
            }
        }
		
		return null;
	}
}








