module GameLogic

type Player =
    PlayerX | PlayerO

type Cell =
    | Empty
    | Player of Player

type Board = Cell list list

type GameOver =
    | Winner of Player
    | Draw

type GameState =
    | GameOver of GameOver
    | Continue

let updateBoard (x, y) v (board: Board) =
    board
    |> List.mapi (fun x' zs ->
        zs |> List.mapi (fun y' v' ->
            if x' = x && y' = y then
                v
            else
                v'))

let newBoard (width, height): Board =
    List.init width (fun _ -> List.init height (fun _ -> Empty))

let getField (x, y) (board: Board) =
    board
    |> List.tryItem x
    |> Option.bind (List.tryItem y)

let canUpdateBoard (x, y) (board: Board) =
    getField (x, y) board = Some Empty

let checkRowsForWinner v (board: Board) =
    board
    |> List.exists (List.forall ((=)v))

let checkColsForWinner v (board: Board) =
    board
    |> List.transpose
    |> checkRowsForWinner v

let flip f a b = f b a

let checkDiagonalsForWinner v (board: Board) =
    let getField = flip getField board
    let diag1 = [ getField (0, 0); getField (1, 1); getField (2, 2) ]
    let diag2 = [ getField (2, 0); getField (1, 1); getField (0, 2) ]
    [ diag1; diag2 ]
    |> List.exists (List.forall ((=)(Some v)))

let checkForWinner (v: Cell) (board: Board) =
    [ checkRowsForWinner; checkColsForWinner; checkDiagonalsForWinner ]
    |> List.exists (fun x -> x v board)

let checkForDraw (board: Board) =
    board
    |> List.forall (List.forall ((<>)Empty))

let getGameState board =
    if checkForWinner (Player PlayerX) board then
        GameOver (Winner PlayerX)
    else if checkForWinner (Player PlayerO) board then
        GameOver (Winner PlayerO)
    else if checkForDraw board then
        GameOver Draw
    else
        Continue
