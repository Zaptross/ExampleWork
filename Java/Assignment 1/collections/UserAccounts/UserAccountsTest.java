package coll.UserAccounts;

public class UserAccountsTest {

    public static void main(String[] args) {
        try {
            User bob = new User("BOB", "ButtButtButt");
        } catch (Exception e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        try {
            User jeff = new User("jeff", "B");
        } catch (Exception e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

}
