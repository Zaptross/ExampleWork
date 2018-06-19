package junit.Uber;

public class Uber {

    private String name;
    private String model;
    private static double rate;
    private double surgeMultiplier = 1.0;
    private double startTime;
    
    public Uber(String name, String model) {
        this.name = name;
        this.model = model;
    }
    
    public String getDriverName() {
        return name;
    }
    
    public String getCarModel() {
        return model;
    }
    
    public static void setFareRate(double newRate) {
        rate = newRate;
    }
    
    public static double getFareRate() {
        return rate;
    }
    
    public void pickupPassenger() {
        startTime = System.currentTimeMillis();
    }
    
    public double setdownPassenger() {
        double endTime = System.currentTimeMillis();
        return (endTime - startTime)/1000 * rate * surgeMultiplier;
    }

    public void setSurgeMultiplier(double surgeMultiplier) {
        this.surgeMultiplier = surgeMultiplier; 
    }

}
