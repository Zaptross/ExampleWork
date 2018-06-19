package coll.Movies;

import java.util.ArrayList;
import java.util.List;

public class Movie {
	private String title;
	private int year;
	private List<Integer> ratings = new ArrayList<Integer>();

	/**
	 * Constructor. Construct a new movie object.
	 * @param title
	 * @param year
	 */
	public Movie(String title, int year) {
		this.title = title;
		this.year = year;
	}

	/**
	 * Rate the movie.
	 * @param rating
	 */
	public void addRating(int rating) {
	    ratings.add(rating);
	}

	/** 
	 * Get the title of the movie.
	 * @return the title of the movie as a String.
	 */
	public String title() {
		return title;
	}

	/** 
	 * Get the year of the movie.
	 * @return the year of the movie as an int.
	 */
	public int year() {
		return year;
	}

	/** 
	 * Get the average rating of the movie.
	 * @return the average rating of the movie as a double.
	 */
	public double rating() {
		double average = 0;
		
		for (int rating: ratings) {
		    average += rating;
		}
		
	    return average / ratings.size();
	}

}