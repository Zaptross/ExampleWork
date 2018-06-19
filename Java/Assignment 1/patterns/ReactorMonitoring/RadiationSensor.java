package patt.ReactorMonitoring;

import java.util.Observable;
import java.util.Random;

public class RadiationSensor extends Observable {

	private double radiationLast;
	private String location;
	private Random sensor;
    
    /**
	 * Constructs a RadiationSensor object
	 * 
	 * @param location.
	 *            An arbitrary location.
	 * @param seed.
	 *            A seed for the random number generator used to simulate radiation
	 *            readings.
	 */
	public RadiationSensor(String location, int seed) {
	    sensor = new Random(seed);
	    this.location = location;
	}

	/**
	 * Gets the location
	 * 
	 * @return location
	 */
	public String getLocation() {
		return location;
	}

	/**
	 * Gets the radiation
	 * 
	 * @return radiation
	 */
	public double getRadiation() {
		return radiationLast;
	}

	/**
	 * Updates radiation, changes the state to true, and notifies all observers of
	 * the change.
	 */
	public void readRadiation() {
	    radiationLast = sensor.nextDouble() * 10;
	    this.setChanged();
	    this.notifyObservers(radiationLast);
	    
	}

}
