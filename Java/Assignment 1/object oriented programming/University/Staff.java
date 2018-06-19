package oop.University;

public class Staff extends Academic {

    private int hours;
    private final double SALARY = 80000;
    private final double RATE = 40;
    
    public Staff(String name, int id, Title title) {
        super(name, id, title);
    }

    public double getWeeklyPay() {
        if (getTitle() == Title.LECTURER) {
            return SALARY / 52;
        } else {
            return RATE * hours;
        }
    }

    public String toString() {
        return "Staff " + getID() + " works as a " + getTitle().toString();
    }
    
    public void setHours(int hours) {
        this.hours = hours;
    }

}
