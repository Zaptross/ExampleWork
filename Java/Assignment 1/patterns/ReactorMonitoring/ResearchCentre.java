package patt.ReactorMonitoring;

import java.util.Observable;

public class ResearchCentre extends RadiationMonitor {

	private double observationTally;
	private double observationCounter;
    
    /**
	 * Constructs a ResearchCentre object, which observes reactor radiation readings
	 * and constantly prints a report with the current moving average of the
	 * recorded observations.
	 * 
	 * @param location.
	 *            An arbitrary location.
	 */
	public ResearchCentre(String location) {
		super(location);
	}

	/**
	 * Updates the monitor with a new observation and prints a report.
	 */
	public void update(Observable subject, Object o) {
	    observationTally += ((RadiationSensor)subject).getRadiation();
	    observationCounter++;
	    System.out.println(generateReport());
	}

	/**
	 * Generates a report of the current moving average, updated by a new
	 * observation. The moving average is calculated by summing all observations
	 * made so far, and dividing by the quantity of observations so far.
	 */
	public String generateReport() {
	    double movingAverage;
	    if (observationCounter > 0 && observationTally > 0) {
	        movingAverage = observationTally / observationCounter;
	    } else {
	        movingAverage = 0.0000000;
	    }
		return now() + " :: moving average :: " +  String.format("%.4f", movingAverage) + " :: " + getLocation();
	}
}
