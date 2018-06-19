package coll.Matrix;

import java.util.*;

public class Matrix<T> implements Iterable {

    private T matrix[][];
    /**
     * Construct a Matrix object.
     * 
     * @param rows. An int that specifies the number of rows.
     * @param columns. An int that specifies the number of columns.
     */
    @SuppressWarnings("unchecked")
    public Matrix(int rows, int columns) {
        // Populate the matrix with rows and columns
        matrix = (T[][]) new Object[rows][columns]; 
    }

    /**
     * Assigns a value to a given cell, specified by its row, column coordinates.
     * 
     * @param row. An int for the row index with 0-based indexing.
     * @param column. An int for the column index with 0-based indexing.
     * @param value. A generic value to be assigned to the given cell.
     */
    public void insert(int row, int column, T value) {
        matrix[row][column] = value;
    }

    /**
     * Gets the value at a given cell, specified by its row, column coordinates.
     * @param row. An int for the row index with 0-based indexing.
     * @param column. An int for the column index with 0-based indexing.
     * @return value. A generic value located at the given cell.
     */
    public T get(int row, int column) {
        return matrix[row][column];
    }

    /**
     * Gets the total number of cells in the matrix.
     * @return an int equal to the total number of cells in the matrix.
     */
    public int size() {
        return matrix.length * matrix[0].length;
    }
    
    /**
     * Converts the matrix to String format.
     * @return a String representation of the matrix.
     */
    public String toString() {
        String stringForm = new String();
        
        // For each row
        for (int row = 0; row < matrix.length; row++) {
            // For each element in that row
            for (int column = 0; column < matrix[0].length; column++) {
                // Add that element to the end of the string
                stringForm += matrix[row][column].toString();
                // If it's the last element in that row
                if (column == matrix[0].length - 1) {
                    // Append a new line
                    stringForm += "\n";
                // If it's not the last element in that row 
                } else {
                    // Append a tab
                    stringForm += "\t";
                }
            }
        }
        
        return stringForm;
    }

    /**
     * Returns an Iterator object for the matrix. The Iterator should follow the
     * order of row by row. Within each row the order is left to right.
     * @return an Iterator object for the matrix.
     */
    public Iterator<Object> iterator() {
        return new Iterator<Object>() {

            int rowsI = 0;
            int columnsI = -1;
            
            public boolean hasNext() {
                if (rowsI < matrix.length - 1 || columnsI < matrix[0].length - 1) {
                    return true;
                }
                return false;
            }

            public T next() {                
                if (hasNext()) {
                    // If we've not reached the last column in the row, increment the column
                    if (columnsI < matrix[0].length - 1) {
                        columnsI++;
                    // If we have reached the end of the row, reset the column and increment the row
                    } else {
                        columnsI = 0;
                        rowsI++;
                    }
                }
                return matrix[rowsI][columnsI];
            }
            
        };
    }

}








