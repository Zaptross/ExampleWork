package patt.Coffee;

import java.util.ArrayList;

import patt.Coffee.CoffeeFactory.*;

public class Coffee {
	Type type;
	double cost;
	ArrayList<Ingredient> ingredients;

	public Coffee(ArrayList<Ingredient> ingredients, Type type) {
		this.type = type;

		this.ingredients = ingredients;

		double sum = 0;
		for (Ingredient ingredient : ingredients) {
			if (ingredient == Ingredient.ESPRESSO) {
				sum += 0.5;
			} else if (ingredient == Ingredient.MILK) {
				sum += 1.0;
			} else if (ingredient == Ingredient.CHOCOLATE) {
				sum += 1.5;
			} else {
				sum += 0;
			}
		}
		this.cost = sum;

	}

	public double getCost() {
		return cost;
	}

	public double getPrice() {
		if (type == Type.LONG_BLACK) {
			return Type.LONG_BLACK.getPrice();
		} else if (type == Type.FLAT_WHITE) {
			return Type.FLAT_WHITE.getPrice();
		} else if (type == Type.MOCHA) {
			return Type.MOCHA.getPrice();
		}
		return 0;
	}

	public String listIngredients() {
		String string = "";
		for (Ingredient ingredient : ingredients) {
			string += ingredient;
			string += "\n";
		}
		return string;
	}
}