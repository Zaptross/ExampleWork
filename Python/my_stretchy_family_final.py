
#-----Statement of Authorship----------------------------------------#
#
#  This is an individual assessment item.  By submitting this
#  code I agree that it represents my own work.  I am aware of
#  the University rule that a student must not act in a manner
#  which constitutes academic dishonesty as stated and explained
#  in QUT's Manual of Policies and Procedures, Section C/5.3
#  "Academic Integrity" and Section E/2.1 "Student Code of Conduct".
#
#    Student no: ########
#    Student name: Matthew Price
#
#  NB: Files submitted without a completed copy of this statement
#  will not be marked.  All files submitted will be subjected to
#  software plagiarism analysis using the MoSS system
#  (http://theory.stanford.edu/~aiken/moss/).
#
#--------------------------------------------------------------------#



#-----Assignment Description-----------------------------------------#
#
#  MY STRETCHY FAMILY
#
#  This assignment tests your skills at defining functions, processing
#  data stored in lists and performing the arithmetic calculations
#  necessary to display a complex visual image.  The incomplete
#  Python script below is missing a crucial function, "draw_portrait".
#  You are required to complete this function so that when the
#  program is run it produces a portrait of a stick figure family in
#  the style of the car window stickers that have become popular in
#  recent years, using data stored in a list to determine the
#  locations and heights of the figures.  See the instruction
#  sheet accompanying this file for full details.
#
#  Note that this assignment is in two parts, the second of which
#  will be released only just before the final deadline.  This
#  template file will be used for both parts and you will submit
#  only your final solution, whether or not you complete both
#  parts.
#
#--------------------------------------------------------------------#  



#-----Preamble-------------------------------------------------------#
#
# This section imports necessary functions and defines constant
# values used for drawing the background.  You should not change any
# of the code in this section.
#

# Import the functions needed to complete this assignment.  You
# should not need to import any other modules for your solution.

from turtle import *
from math import *

# Define constant values used in the main program that sets up
# the drawing canvas.  Do not change any of these values.

window_height = 550 # pixels
window_width = 900 # pixels
grass_height = 200 # pixels
grass_offset = -100 # pixels
location_width = 150 # pixels
num_locations = 5

#
#--------------------------------------------------------------------#



#-----Functions for Drawing the Background---------------------------#
#
# The functions in this section are called by the main program to
# draw the background and the locations where the individuals in the
# portrait are required to stand.  You should not change any of
# the code in this section.  Note that each of these functions
# leaves the turtle's pen up.
#


# Draw the grass as a big green rectangle
def draw_grass():
    
    penup()
    goto(-window_width / 2, grass_offset) # start at the bottom-left
    setheading(90) # face north
    fillcolor('pale green')
    begin_fill()
    forward(grass_height)
    right(90) # face east
    forward(window_width)
    right(90) # face south
    forward(grass_height)
    right(90) # face west
    forward(window_width)
    end_fill()


# Draw the locations where the individuals must stand
def draw_locations(locations_on = True):

    # Only draw the locations if the argument is True
    if locations_on:

        # Define a small gap at each end of each location
        gap_size = 5 # pixels
        location_width_less_gaps = location_width - (gap_size * 2) # pixels

        # Start at the far left, facing east
        penup()
        goto(-num_locations * location_width / 2, 0)
        setheading(0) 
  
        # Draw each location as a thick line with a gap at each end
        color('dark khaki')
        for location in range(num_locations):
            penup()
            forward(gap_size)
            pendown()
            width(5) # draw a thick line
            forward(location_width_less_gaps)
            width(1)
            penup()
            forward(gap_size)


# Draw the numeric labels on the locations
def draw_labels(labels_on = True):

    # Only draw the labels if the argument is True
    if labels_on:
    
        font_size = 16 # size of characters for the labels

        # Start in the middle of the left-hand location, facing east
        penup()
        goto(-((num_locations - 1) * location_width) / 2,
             -font_size * 2)
        setheading(0) 

        # Walk to the right, print the labels as we go
        color('dark khaki')
        for label in range(num_locations):
            write(label, font = ('Arial', font_size, 'bold'))
            forward(location_width)


# As a debugging aid, mark certain absolute coordinates on the canvas
def mark_coords(marks_on = True):

    # Only mark the coordinates if the argument is True
    if marks_on:

        # Mark the "home" coordinate
        home()
        width(1)
        color('black')
        dot(3)
        write('0, 0', font = ('Arial', 10, 'normal'))

        # Mark the centre point of each individual's location
        max_x = (num_locations - 1) * location_width / 2
        penup()
        for x_coord in range(-max_x, max_x + location_width, location_width):
            for y_coord in [0, 400]:
                goto(x_coord, y_coord)
                dot(3)
                write(str(x_coord) + ', ' + str(y_coord),
                      font = ('Arial', 10, 'normal'))
                
#
#--------------------------------------------------------------------#



#-----Test data------------------------------------------------------#
#
# These are the data sets you will use to test your code.
# Each of the data sets is a list specifying the positions for
# the people in the portrait:
#
# 1. The name of the individual, from 'Person A' to 'Person D' or 'Pet'
# 2. The place where that person/pet must stand, from location 0 to 4
# 3. How much to stretch the person/pet vertically, from 0.5 to 1.5
#    times their normal height
# 4. A mystery value, either '*' or '-', whose purpose will be
#    revealed only in the second part of the assignment
#
# Each data set does not necessarily include all people and sometimes
# they require the same person to be drawn more than once.  You
# can assume, however, that they never put more than one person in
# the same location.
#
# You may add additional data sets but you may not change any of the
# given data sets below.
#

# The following data set doesn't require drawing any people at
# all.  You may find it useful as a dummy argument when you
# first start developing your "draw_portrait" function.

portrait_00 = []

# The following data sets each draw just one of the individuals
# at their default height.

portrait_01 = [['Person A', 2, 1.0, '-']]

portrait_02 = [['Person B', 3, 1.0, '-']]

portrait_03 = [['Person C', 1, 1.0, '-']]

portrait_04 = [['Person D', 0, 1.0, '-']]

portrait_05 = [['Pet', 4, 1.0, '-']]

# The following data sets each draw just one of the individuals
# but multiple times and at different sizes.

portrait_06 = [['Person A', 3, 1.0, '-'],
               ['Person A', 1, 0.75, '-'],
               ['Person A', 2, 0.5, '-'],
               ['Person A', 4, 1.4, '-']]

portrait_07 = [['Person B', 0, 0.5, '-'],
               ['Person B', 2, 1.0, '-'],
               ['Person B', 3, 1.5, '-']]

portrait_08 = [['Person C', 0, 0.5, '-'],
               ['Person C', 1, 0.75, '-'],
               ['Person C', 2, 1.0, '-'],
               ['Person C', 3, 1.25, '-'],
               ['Person C', 4, 1.5, '-']]

portrait_09 = [['Person D', 3, 1.25, '-'],
               ['Person D', 1, 0.8, '-'],
               ['Person D', 0, 1.0, '-']]

portrait_10 = [['Pet', 1, 1.3, '-'],
               ['Pet', 2, 1.0, '-'],
               ['Pet', 3, 0.7, '-']]

# The following data sets each draw a family portrait with all
# individuals at their default sizes.  These data sets create
# "natural" looking portraits.  Notably, the first two both
# show the full family.

portrait_11 = [['Person A', 0, 1.0, '-'],
               ['Person B', 1, 1.0, '-'],
               ['Person C', 2, 1.0, '*'],
               ['Person D', 3, 1.0, '-'],
               ['Pet', 4, 1.0, '-']]

portrait_12 = [['Person A', 2, 1.0, '-'],
               ['Person B', 3, 1.0, '*'],
               ['Person C', 1, 1.0, '-'],
               ['Person D', 4, 1.0, '-'],
               ['Pet', 0, 1.0, '-']]

portrait_13 = [['Person B', 1, 1.0, '-'],
               ['Pet', 2, 1.0, '-'],
               ['Person C', 3, 1.0, '*']]

portrait_14 = [['Person C', 0, 1.0, '-'],
               ['Pet', 1, 1.0, '-'],
               ['Person A', 2, 1.0, '*'],
               ['Person D', 3, 1.0, '-'],
               ['Person B', 4, 1.0, '-']]

portrait_15 = [['Person D', 4, 1.0, '*'],
               ['Person A', 3, 1.0, '-'],
               ['Person B', 2, 1.0, '-']]

portrait_16 = [['Person D', 1, 1.0, '-'],
               ['Person C', 0, 1.0, '-'],
               ['Person A', 2, 1.0, '-'],
               ['Person B', 3, 1.0, '*']]

# The following data sets draw all five individuals at their
# minimum and maximum heights.

portrait_17 = [['Person A', 0, 0.5, '-'],
               ['Person B', 1, 0.5, '-'],
               ['Person C', 2, 0.5, '*'],
               ['Person D', 3, 0.5, '-'],
               ['Pet', 4, 0.5, '-']]

portrait_18 = [['Person A', 4, 1.5, '-'],
               ['Person B', 3, 1.5, '*'],
               ['Person C', 2, 1.5, '-'],
               ['Person D', 1, 1.5, '-'],
               ['Pet', 0, 1.5, '-']]

# The following data sets each draw a family portrait with
# various individuals at varying sizes.

portrait_19 = [['Person A', 0, 0.5, '*'],
               ['Person B', 1, 0.8, '-'],
               ['Person C', 2, 1.5, '-'],
               ['Person D', 3, 1.5, '-'],
               ['Pet', 4, 0.5, '-']]

portrait_20 = [['Person B', 1, 0.8, '*'],
               ['Pet', 2, 1.4, '-'],
               ['Person C', 3, 0.7, '-']]

portrait_21 = [['Person C', 0, 1.5, '-'],
               ['Pet', 1, 1.0, '-'],
               ['Person A', 2, 1.5, '-'],
               ['Person D', 3, 1.5, '*'],
               ['Person B', 4, 1.5, '-']]

portrait_22 = [['Person D', 4, 1.2, '-'],
               ['Person A', 3, 1.0, '*'],
               ['Person B', 2, 0.8, '-']]

portrait_23 = [['Person D', 1, 1.1, '-'],
               ['Person C', 2, 0.9, '-'],
               ['Person A', 0, 1.1, '*'],
               ['Person B', 3, 0.9, '-']]

# ***** If you want to create your own data sets you can add them here
# ***** (but your code must still work with all the data sets above plus
# ***** any other data sets in this style).
portrait_custom = [['Mum', 1, 1.0, '-'],
                   ['Dad', 2, 1.2, '-'],
                   ['Me', 3, 1.3, '-'],
                   ['Sister', 4, 1.1, '*']]
#
#--------------------------------------------------------------------#



#-----Student's Solution---------------------------------------------#
#
#  Complete the assignment by replacing the dummy function below with
#  your own "draw_portrait" function.
#
def draw_portrait(portrait): # draws all characters in portrait
    for character in portrait:
        draw_stick_figure(character)
# Draw the stick figures as per the provided data set
def isFemale(pronoun): #checks if it's female name
    female_pronouns = [ #enumerates names which indicate a female character
                "Mum",
                "Mom",
                "Wife",
                "Sister",
                "Girl",
                "Person A",
                "Person C",
                "Daughter"
                ]
    try: #run code and catch errors, list.index returns an error if it can't match the string
        if female_pronouns.index(pronoun) > -1: #check input string against female_pronouns table
            return True
    except:
        return False

def getItems(name): #checks if it's female name
    item_Index = [
                 "",
                 "Dad", "Father", "Person D",
                 "Me", "Brother", "Person B",
                 "Mum", "Mom", "Wife", "Person A",
                 "Sister", "Daughter", "Girl", "Person C"
                 ]

    try: #run code and catch errors, list.index returns an error if it can't match the string
        #check input string against item_Index table, return index
        return item_Index.index(name)
    except:
        return 0

def draw_stick_figure(draw_settings):
    #initialise some variables for later use
    draw_Coffee = False
    draw_Crown = False
    draw_Dress = False
    draw_Tie = False
    draw_Pearls = False

    #read list of settings for this character, split into components
    name = draw_settings[0]
    position = draw_settings[1]
    scaling_modifier = draw_settings[2]
    has_crown = draw_settings[3]

    #calculates the horizontal offset to place the drawn character
    base_offset = -300 + position * 150

    stick_vectors = [ #vectors for drawing a base stick figure
                    [(-50,0),(0,100)],
                    [(50,0),(0,100)],
                    [(0,100),(0,200)],
                    [(-50,120),(0,200)],
                    [(50,120),(0,200)],
                    [[0,200,30]]
                    ]

    dress_vectors = [ #vectors for drawing a dress
                    [(0,200),(-50,50)],
                    [(-50,50),(50,50)],
                    [(50,50),(0,200)]
                    ]

    tie_vectors = [ #vectors for drawing a tie
                  [(0,200),(-10,140)],
                  [(-10,140),(0,130)],
                  [(0,130),(10,140)],
                  [(10,140),(0,200)],
                  [[0,194,4]]
                  ]

    coffee_vectors = [ #vectors for drawing a coffee arm
                     [(20,150),(40,200)],
                     [(40,195),(40,205)],
                     [(40,195),(44,195)],
                     [(40,205),(44,205)],
                     [(44,193),(44,208)],
                     [(44,208),(55,208)],
                     [(55,208),(55,193)],
                     [(55,193),(44,193)],
                     [(20,150),(0,200)] #right arm replacement
                     ]

    pearl_vectors = [ #vectors for drawing a pearl necklace
                     [[-4,196,2]],
                     [[4,196,2]],
                     [[-3,192,2]],
                     [[3,192,2]],
                     [[0,189,2]]
                     ]

    crown_vectors = [ #vectors for drawing a crown
                     [(-20,255),(20,255)],
                     [(20,255),(20,265)],
                     [(20,265),(15,260)],
                     [(15,260),(10,265)],
                     [(10,265),(5,260)],
                     [(5,260),(0,265)],
                     [(0,265),(-5,260)],
                     [(-5,260),(-10,265)],
                     [(-10,265),(-15,260)],
                     [(-15,260),(-20,265)],
                     [(-20,265),(-20,255)]
                     ]

    dress_colour_list = [ #colours for dresses
                        "pink",
                        "red",
                        "green",
                        "yellow",
                        "cyan"
                        ]

    if isFemale(name): #checks name for gender, appends vectors to draw a dress
        draw_Dress = True
        for vector in dress_vectors:
            stick_vectors.append(vector)
        dress_index = dress_vectors[0]
    else:
        dress_index = None

    if has_crown == '*': #checks has_crown, draws crown if applicable
        draw_Crown = True
        for vector in crown_vectors:
            stick_vectors.append(vector)
        crown_index = crown_vectors[0]
    else:
        crown_index = None

    if getItems(name) > 0 and getItems(name) < 4: #checks name and draws a tie on a character
        draw_Tie = True
        for vector in tie_vectors:
            stick_vectors.append(vector)
        tie_index = tie_vectors[0]
    else:
        tie_index = None

    if getItems(name) > 3 and getItems(name) < 7: #checks name and draws modified arm holding coffee on a character
        stick_vectors[4] = coffee_vectors.pop()
        draw_Coffee = True
        for vector in coffee_vectors:
            stick_vectors.append(vector)
        coffee_index = coffee_vectors[4]
    else:
        coffee_index = None

    if getItems(name) > 6 and getItems(name) < 11: #checks name and draws pearls on a character
        draw_Pearls = True
        for pearl in pearl_vectors:
            stick_vectors.append(pearl)
        pearl_index = pearl_vectors[0]
    else:
        pearl_index = None

    #initially set the lines to black and fill to white
    color("black","white")

    #for each element in the stick vectors, draw the line, fill if necessary
    for vector in stick_vectors:
        pu() #stop drawing, to prevent drawing errors

        x = vector[0][0] + base_offset #set line start coords, modify for scale, offset
        y = vector[0][1] * scaling_modifier

        #check conditions and set colours accordingly
        if vector == dress_index and draw_Dress:
            dress_colour = dress_colour_list[position]
            fillcolor(dress_colour)
            fill(True)
        elif vector == tie_index and draw_Tie:
            fill(True)
            color("black","red")
        elif vector == coffee_index and draw_Coffee:
            fill(True)
            color("black","brown")
        elif vector == pearl_index and draw_Pearls:
            color("black","white")
        elif vector == crown_index and draw_Crown:
            fill(True)
            color("black","yellow")

        #check if drawing a line, else draw a circle
        if len(vector) == 2:
            #set line end coords, modify for scale, offset
            x2 = vector[1][0] + base_offset
            y2 = vector[1][1] * scaling_modifier

            #go to the start of the line, draw the line, stop drawing
            goto(x,y)
            pd()
            goto(x2,y2)
            pu()
        else:
            fill(False) #turn filling off, to stop filling outside the circle
            #go to the start of the circle, draw the circle, stop drawing
            radius = vector[0][2] * scaling_modifier
            goto(x,y)
            setheading(0)
            fill(True) #fill circles separately
            pd()
            circle(radius)
            pu()
            fill(False) #turn filling off to stop overflow

    fill(False) #setting filling objects to false, stopping filling between characters
#
#--------------------------------------------------------------------#



#-----Main Program---------------------------------------------------#
#
# This main program sets up the background, ready for you to start
# drawing your stick figures.  Do not change any of this code except
# where indicated by comments marked '*****'.
#
    
# Set up the drawing window with a blue background representing
# the sky, and with the "home" coordinate set to the middle of the
# area where the stick figures will stand
setup(window_width, window_height)
setworldcoordinates(-window_width / 2, grass_offset,
                    window_width / 2, window_height + grass_offset)
bgcolor('sky blue')

# Draw the grass (with animation turned off to make it faster)
tracer(False)
draw_grass()

# Give the window a title
# ***** Replace this title with one that describes your choice
# ***** of individuals
title('My Stretchy Family: Mum, Dad, Me and Sister')

# Control the drawing speed
# ***** Modify the following argument if you want to adjust
# ***** the drawing speed
speed('fastest')

# Draw the locations to stand, their labels and selected coordinates
# ***** If you don't want to display these background elements,
# ***** to make your portrait look nicer, change the corresponding
# ***** argument(s) below to False
draw_locations(False)
draw_labels(False)
mark_coords(False)

# Call the student's function to display the stick figures
# ***** If you want to turn off animation while drawing your
# ***** stick figures, to make your program draw faster, change
# ***** the following argument to False
tracer(False)
# ***** Change the argument to this function to test your
# ***** code with different data sets
### Leaves colour as black and black, pen up
draw_portrait(portrait_custom)
# Exit gracefully by hiding the cursor and releasing the window
tracer(True)
hideturtle()
done()

#
#--------------------------------------------------------------------#

