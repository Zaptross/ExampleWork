module SegmentModule

type Coordinate = (int * int) // x, y coordinate of a pixel
type Colour = byte list       // one entry for each colour band, typically: [red, green and blue]

type Segment = 
    | Pixel of Coordinate * Colour
    | Parent of Segment * Segment 

// Lecturer's transpose list function
let transposeList (rows:list<list<'T>>) : list<list<'T>> =
    let n = List.length (List.head rows)
    List.init n (fun i -> (List.map (List.item i) rows))

// Find standard deviation of a list of floats
let deviation (floats: list<float>) : float =
    floats 
    // Subtract the average from each element of the list
    |> List.map (fun x -> (x - (List.sum floats / float (List.length floats))))
    // Square each element of the list
    |> List.map (fun x -> x * x)
    // Average the current list, then square root it
    |> fun x -> (sqrt (List.sum x / float (List.length x)))

// Recursively get the colour lists from each segment and return all of them as a list of lists
let rec getPixelColours (segment: Segment) : List<List<float>> =
    match segment with
    // If it's a parent, recursively call this function and append the two results
    | Parent (segmentOne, segmentTwo) -> getPixelColours(segmentOne) @ getPixelColours(segmentTwo)
    // If it's a pixel, convert it to a float list, and return it contained in a list
    | Pixel (coordinate, colour) -> [colour |> List.map float]

// return a list of the standard deviations of the pixel colours in the given segment
// the list contains one entry for each colour band, typically: [red, green and blue]
let stddev (segment: Segment) : float list =
    getPixelColours(segment)
    |> transposeList
    |> List.map (deviation)

// determine the cost of merging the given segments: 
// equal to the standard deviation of the combined the segments minus the sum of the standard deviations of the individual segments, 
// weighted by their respective sizes and summed over all colour bands
let mergeCost segment1 segment2 : float = 
    // Weight the first and second segments stddev's by the number of pixels in them
    let segOneWeighted = (List.sum(stddev(segment1)) * (float(List.length(getPixelColours(segment1)))))
    let segTwoWeighted = (List.sum(stddev(segment2)) * (float(List.length(getPixelColours(segment2)))))
    // Find the total deviation of the combined segments
    let segCombined = (List.sum(stddev(Parent(segment1, segment2))) * (float(List.length(getPixelColours(Parent(segment1, segment2))))))
    // Return the "cost" of merging them
    segCombined - (segOneWeighted + segTwoWeighted)