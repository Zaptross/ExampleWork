package coll.EnrollmentManager;

public class EnrollmentTest {

    public static void main(String[] args) {
        EnrollmentManager.enroll("CAB302", "Matt");
        
        EnrollmentManager.getStudents("CAB302");
        
        EnrollmentManager.withdrawEnrollment("CAB302", "Matt");

    }

}
