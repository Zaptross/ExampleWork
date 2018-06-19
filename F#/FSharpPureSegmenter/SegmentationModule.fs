module SegmentationModule

open SegmentModule
open TiffModule

// Maps segments to their immediate parent segment that they are contained within (if any) 
type Segmentation = Map<Segment, Segment>

// Find the largest/top level segment that the given segment is a part of (based on the current segmentation)
let rec findRoot (segmentation: Segmentation) segment : Segment =
    match segmentation.ContainsKey(segment) with
    | false -> segment // If this segmentation has no parent, it must be the root
    | true -> findRoot(segmentation) segmentation.[segment] //If it has a parent, go a layer deeper


// Initially, every pixel/coordinate in the image is a separate Segment
// Note: this is a higher order function which given an image, 
// returns a function which maps each coordinate to its corresponding (initial) Segment (of kind Pixel)
let createPixelMap (image:TiffModule.Image) : (Coordinate -> Segment) =
    fun coordinate -> Pixel(coordinate, getColourBands(image) coordinate)




// Find the neighbouring segments of the given segment (assuming we are only segmenting the top corner of the image of size 2^N x 2^N)
// Note: this is a higher order function which given a pixelMap function and a size N, 
// returns a function which given a current segmentation, returns the set of Segments which are neighbours of a given segment
let createNeighboursFunction (pixelMap:Coordinate->Segment) (N:int) : (Segmentation -> Segment -> Set<Segment>) =
    // Find the coordinates of neighbouring pixels
    let rec neighbouringPixels segment =
        let width = 1 <<< N
        let height = 1 <<< N
    
        match segment with
        | Parent (segmentOne, segmentTwo) -> neighbouringPixels(segmentOne) |> Seq.append(neighbouringPixels(segmentTwo))
        | Pixel ((xCoord, yCoord), colour) -> 
        seq {
            if xCoord + 1 < width then yield Coordinate(xCoord + 1, yCoord)
            if xCoord - 1 >= 0 then yield Coordinate(xCoord - 1, yCoord)
            if yCoord + 1 < height then yield Coordinate(xCoord, yCoord + 1)
            if yCoord - 1 >= 0 then yield Coordinate(xCoord, yCoord - 1)
        }

    let neighbours segmentation segment = 
        neighbouringPixels(segment)
        |> Seq.map(pixelMap)
        |> Seq.map(findRoot segmentation)
        |> Set.ofSeq
        |> Set.remove(findRoot segmentation segment)
    
    neighbours // return neighbours function


// The following are also higher order functions, which given some inputs, return a function which ...


 // Find the neighbour(s) of the given segment that has the (equal) best merge cost
 // (exclude neighbours if their merge cost is greater than the threshold)
let createBestNeighbourFunction (neighbours:Segmentation->Segment->Set<Segment>) (threshold:float) : (Segmentation->Segment->Set<Segment>) =
    let findBestNeighbour (threshold:float) (neighbours:List<Segment>) (segment:Segment) (bestNeighbours:Set<Segment>)= 
        match neighbours with
        | [] -> bestNeighbours // Catch a list of no neighbours
        | _ -> neighbours
            |> List.filter(fun element -> mergeCost segment element < threshold)
            |> fun x -> match x with
                | [] -> bestNeighbours // Catch a list of no neighbours that are below the threshold
                | _ -> x
                    |> List.minBy(fun element -> mergeCost segment element)
                    |> bestNeighbours.Add

    let bestNeighbours segmentation segment =
        let allNeighbours =
            neighbours segmentation segment
            |> Set.toList
        findBestNeighbour threshold allNeighbours segment (Set [])

    bestNeighbours


// Try to find a neighbouring segmentB such that:
//     1) segmentB is one of the best neighbours of segment A, and 
//     2) segmentA is one of the best neighbours of segment B
// if such a mutally optimal neighbour exists then merge them,
// otherwise, choose one of segmentA's best neighbours (if any) and try to grow it instead (gradient descent)
let createTryGrowOneSegmentFunction (bestNeighbours:Segmentation->Segment->Set<Segment>) (pixelMap:Coordinate->Segment) : (Segmentation->Coordinate->Segmentation) =
    
    let tryGrowSegment (segmentation:Segmentation) (coordinate:Coordinate) =
        
        // Merge two segment roots together to make a new segmentation, return it
        let merge (segmentOne:Segment) (segmentTwo) : Segmentation =
            let segmentOneRoot = findRoot segmentation segmentOne
            let segmentTwoRoot = findRoot segmentation segmentTwo
            let parent = Parent (segmentOneRoot, segmentTwoRoot) // Create a new parent to store
            (segmentation.Add (segmentOneRoot, parent)).Add (segmentTwoRoot, parent) // Add new references to the segment
    
        // Compare two segments recursively until a pair of best neighbours is found
        let rec findMutualNeighbour (rootSegment:Segment) (segmentBestNeighbours:List<Segment>) =
            match segmentBestNeighbours with
                | [] -> None // Catch empty list
                | head::_ when (bestNeighbours segmentation head).Contains rootSegment -> // Check if the best neighbours contain the root from the first to second last element
                    Some(merge rootSegment segmentBestNeighbours.Head) // Merge them if they do pair
                | _::tail -> findMutualNeighbour rootSegment segmentBestNeighbours.Tail // Otherwise, go a layer deeper

        // Triest to find a pair to merge, if can't find, returns current segmentation
        let rec tryMerge (segment:Segment) =
            let segmentNeighbours = Set.toList (bestNeighbours segmentation segment)
            if segmentNeighbours.IsEmpty then segmentation
                else match findMutualNeighbour segment segmentNeighbours with
                    | None -> tryMerge segmentNeighbours.Head
                    | Some(segmentation) -> segmentation

        pixelMap coordinate
        |> findRoot segmentation
        |> tryMerge

    tryGrowSegment


// Try to grow the segments corresponding to every pixel on the image in turn 
// (considering pixel coordinates in special dither order)
let createTryGrowAllCoordinatesFunction (tryGrowPixel:Segmentation->Coordinate->Segmentation) (N:int) : (Segmentation->Segmentation) =
    
    let tryGrowAllCoordinates (segmentation:Segmentation) =
        // Recursively, try to grow the next 
        let rec tryGrowCoordinates (segmentation:Segmentation) (orderList:List<int*int>) =
            match orderList with
            | [] -> segmentation
            | _ -> tryGrowCoordinates (tryGrowPixel segmentation orderList.Head) orderList.Tail
        
        tryGrowCoordinates segmentation (DitherModule.coordinates N |> List.ofSeq)

    tryGrowAllCoordinates


// Keep growing segments as above until no further merging is possible
let createGrowUntilNoChangeFunction (tryGrowAllCoordinates:Segmentation->Segmentation) : (Segmentation->Segmentation) =
    
    let rec growUntilNoChangeFunction (segmentation:Segmentation) =
        let newSegmentation = tryGrowAllCoordinates segmentation
        match segmentation with
        | segmentation when segmentation.Equals newSegmentation -> newSegmentation
        | _ -> growUntilNoChangeFunction newSegmentation

    growUntilNoChangeFunction
                


// Segment the given image based on the given merge cost threshold, but only for the top left corner of the image of size (2^N x 2^N)
let segment (image:TiffModule.Image) (N: int) (threshold:float)  : (Coordinate -> Segment) =
    
    let pixelMap = createPixelMap image
    let neighbours = createNeighboursFunction pixelMap N
    let bestNeighbours = createBestNeighbourFunction neighbours threshold
    let tryGrowOneSegment = createTryGrowOneSegmentFunction bestNeighbours pixelMap
    let tryGrowAllCoordinates = createTryGrowAllCoordinatesFunction tryGrowOneSegment N
    let growUntilNoChange = createGrowUntilNoChangeFunction tryGrowAllCoordinates    
    let segmentation = growUntilNoChange Map.empty

    let getSegment (coords:Coordinate) =
        findRoot segmentation (pixelMap coords)
    getSegment