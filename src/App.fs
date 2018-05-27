module App

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open GameLogic

importSideEffects "./App.scss"
let playerXImage = importAll "./assets/PlayerX.svg"
let playerOImage = importAll "./assets/PlayerO.svg"
let restartImage = importAll "./assets/restart.png"

[<Fable.Core.Pojo>]
type Model =
    { board: Board
      currentPlayer: Player
      gameState: GameState }

type Position = Position of int * int
with override self.ToString() =
        let (Position (x, y)) = self
        sprintf "%i-%i" x y

[<Fable.Core.Pojo>]
type Msg =
    | Move of Position
    | Restart

type OnMove = Position -> unit

module State =
    let init () =
        { board = GameLogic.newBoard (3, 3)
          currentPlayer = PlayerX
          gameState = Continue }

    let nextPlayer = function
        | PlayerX -> PlayerO
        | PlayerO -> PlayerX

    let nextMove (Position (x, y)) model =
        if GameLogic.canUpdateBoard (x, y) model.board then
            let newBoard =
                GameLogic.updateBoard (x, y) (Player model.currentPlayer) model.board
            let nextPlayer = nextPlayer model.currentPlayer
            let newGameState = GameLogic.getGameState newBoard
            { model with
                board = newBoard
                currentPlayer = nextPlayer
                gameState = newGameState }
        else model

    let update msg model =
        match msg with
        | Move position -> nextMove position model
        | Restart -> init ()

module View =
    let player src className =
        img [ ClassName className; Src src ]

    let playerX = player playerXImage
    let playerO = player playerOImage
    let playerXM = playerX "PlayerX"
    let playerOM = playerO "PlayerO"
    let playerXS = playerX "PlayerX PlayerX--Small"
    let playerOS = playerO "PlayerO PlayerO--Small"

    let dispatchCellValue = function
        | Empty -> span [ ClassName "NoPlayer" ] []
        | Player PlayerX -> playerXM
        | Player PlayerO -> playerOM

    let cell (onMove: OnMove) position (cell': Cell) =
        button
            [ ClassName "Cell"
              OnClick (fun _ -> onMove position)
              Key (string position) ]
            [ dispatchCellValue cell' ]

    let row onMove x row' =
        div [ ClassName "Board__Row"
              Key (string x) ]
            (List.mapi (fun y ->
                cell onMove (Position (x, y))) row')

    let board onMove (board': Board) =
        div [ ClassName "Board" ]
            (List.mapi (row onMove) board')

    let currentPlayer currentPlayer =
        div [ ClassName "CurrentPlayer" ]
            [ span [ ClassName "CurrentPlayer__Text" ]
                   [ yield str "Player: "
                     yield match currentPlayer with
                           | PlayerX -> playerXS
                           | PlayerO -> playerOS ] ]

    let game onMove board' currentPlayer' =
        div []
            [ board onMove board'
              currentPlayer currentPlayer' ]

    let gameOverPlayer player =
        span []
            [ yield str "Player"
              yield match player with
                    | PlayerX -> playerXS
                    | PlayerO -> playerOS
              yield str "wins!" ]

    let gameOver onRestart gameOver' =
        div [ ClassName "GameOver" ]
            [ img [ ClassName "GameOver__Image"
                    OnClick (fun _ -> onRestart ())
                    Src restartImage
                    Alt "Restart" ]
              p [ ClassName "GameOver__Text" ]
                [ match gameOver' with
                  | Draw ->
                    yield str "It's a draw"
                  | Winner player ->
                    yield gameOverPlayer player ] ]
    let prefetchImages =
        [ playerOImage; playerXImage ]
        |> List.map (fun img -> link [ Rel "prefetch"
                                       Href img ])

    let root model dispatch =
        div [ ClassName "App" ]
            [ yield! prefetchImages
              yield match model.gameState with
                    | Continue ->
                        game (dispatch << Move) model.board model.currentPlayer
                    | GameOver gameOver' ->
                        gameOver (fun _ -> dispatch Restart) gameOver' ]

open Elmish
open Elmish.React
open Elmish.Debug

Program.mkSimple State.init State.update View.root
|> Program.withReact "root"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
