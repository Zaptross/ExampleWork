package coll.UserAccounts;

import java.util.ArrayList;

public class User {

    public static ArrayList<String> usernames = new ArrayList<String>(); 
    
    private String username;
    private String password;
    
    /**
     * Constructs a user with a given username and password. If a user is
     * successfully constructed, their username is added to the list of usernames.
     * 
     * @param username
     * @param password
     * @throws Exception.
     *             Throws a UserException if the username or password is invalid
     *             (use the below methods badUsername and badPassword to assist you
     *             with this).
     */
    public User(String username, String password) throws Exception {
        
        if (!badUsername(username)) {
            this.username = username;
            usernames.add(username);
        } else {
            throw new UserException();
        }
        
        if (!badPassword(password)) {
            this.password = password;   
        } else {
            throw new UserException();
        }
    }

    /**
     * Gets the username of the user.
     * 
     * @return
     */
    public String getUsername() {
        return username;
    }

    /**
     * Gets the password of the user.
     * 
     * @return
     */
    public String getPassword() {
        return password;
    }

    /**
     * Checks if a given username is invalid
     * 
     * @param username
     * @return true if the username already exist, false otherwise.
     */
    public static boolean badUsername(String username) {
        return usernames.contains(username);
    }

    /**
     * Checks if a given password is invalid.
     * 
     * @param password
     * @return true if the password is fewer than 8 characters long, false
     *         otherwise.
     */
    public static boolean badPassword(String password) {
        return password.length() < 8;
    }

}