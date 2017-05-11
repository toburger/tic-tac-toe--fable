module App

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fable.Import.React
open GameLogic
open Fable.Import.Recompose

importAll "./App.scss"
let playerXImage = importAll "./assets/PlayerX.svg"
let playerOImage = importAll "./assets/PlayerO.svg"

type Model =
    { board: Board
      currentPlayer: Player
      gameState: GameState }

type Position = Position of int * int with
    override self.ToString() =
        let (Position (x, y)) = self
        sprintf "%i-%i" x y

type Msg =
    | Move of Position
    | Restart

type OnMove = Position -> unit

let init () =
    { board = GameLogic.newBoard (3, 3)
      currentPlayer = PlayerX
      gameState = Continue }

let nextPlayer = function
    | PlayerX -> PlayerO
    | PlayerO -> PlayerX

let nextMove (Position (x, y)) model =
    let newBoard = GameLogic.updateBoard (x, y) (Player model.currentPlayer) model.board
    let nextPlayer = nextPlayer model.currentPlayer
    let newGameState = GameLogic.getGameState newBoard
    { model with
        board = newBoard
        currentPlayer = nextPlayer
        gameState = newGameState }

let update msg model =
    match msg with
    | Move position -> nextMove position model
    | Restart -> init ()

let dispatchCellValue = function
    | Empty -> span [ ClassName "NoPlayer" ] []
    | Player PlayerX -> img [ ClassName "PlayerX"; Src playerXImage ]
    | Player PlayerO -> img [ ClassName "PlayerO"; Src playerOImage ]

let cell (onMove: OnMove) position (cell': Cell) =
    button
        [ ClassName "Cell"; OnClick (fun _ -> onMove position); Key (string position) ]
        [ dispatchCellValue cell' ]

let row onMove x row' =
    div [ ClassName "Board__Row"; Key (string x) ] (List.mapi (fun y -> cell onMove (Position (x, y))) row')

let board onMove (board': Board) =
    div [ ClassName "Board" ] (List.mapi (row onMove) board')

let gameOver gameState =
    match gameState with
    | Continue -> str ""
    | Draw -> str "We have a draw"
    | Winner PlayerX -> str "Player X wins!"
    | Winner PlayerO -> str "Player O wins!"

let prefetchImages =
    [ playerOImage; playerXImage ]
    |> List.map (fun img -> link [ Rel "prefetch"; Href img ])

let root model dispatch =
    div [ ClassName "App" ]
        [ yield! prefetchImages
          match model.gameState with
          | Continue -> yield board (dispatch << Move) model.board
          | gameOver' -> yield gameOver gameOver' ]

open Elmish
open Elmish.React

Program.mkSimple init update root
|> Program.withReact "root"
|> Program.run
