module List

let rec transpose = function
    | (_::_)::_ as m -> List.map List.head m :: transpose (List.map List.tail m)
    | _ -> []

let windowed windowSize xs =
    let lngth = List.length xs
    if lngth <= windowSize then []
    else
        [0..(lngth - windowSize)]
        |> List.map(fun j -> xs.[j..(j + windowSize - 1)])
