import PlayerX from "/assets/PlayerX.svg";
import PlayerO from "/assets/PlayerO.svg";
import restart from "/assets/restart.png";
import { toString, Union, Record } from "./.fable/fable-library.3.1.1/Types.js";
import { getGameState, Cell, updateBoard, canUpdateBoard, GameState, Player, newBoard as newBoard_1, GameState$reflection, Player$reflection, Cell$reflection } from "./GameLogic.fs.js";
import { union_type, int32_type, record_type, list_type } from "./.fable/fable-library.3.1.1/Reflection.js";
import { printf, toText } from "./.fable/fable-library.3.1.1/String.js";
import * as react from "react";
import { int32ToString } from "./.fable/fable-library.3.1.1/Util.js";
import { ofSeq, mapIndexed } from "./.fable/fable-library.3.1.1/List.js";
import { singleton, append, delay } from "./.fable/fable-library.3.1.1/Seq.js";
import { ProgramModule_mkSimple, ProgramModule_run } from "./.fable/Fable.Elmish.3.0.0/program.fs.js";
import { Program_withReactBatched } from "./.fable/Fable.Elmish.React.3.0.1/react.fs.js";
import "./App.scss";


export const playerXImage = PlayerX;

export const playerOImage = PlayerO;

export const restartImage = restart;

export class Model extends Record {
    constructor(board, currentPlayer, gameState) {
        super();
        this.board = board;
        this.currentPlayer = currentPlayer;
        this.gameState = gameState;
    }
}

export function Model$reflection() {
    return record_type("App.Model", [], Model, () => [["board", list_type(list_type(Cell$reflection()))], ["currentPlayer", Player$reflection()], ["gameState", GameState$reflection()]]);
}

export class Position extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Position"];
    }
    toString() {
        const self = this;
        const y = self.fields[1] | 0;
        const x = self.fields[0] | 0;
        return toText(printf("%i-%i"))(x)(y);
    }
}

export function Position$reflection() {
    return union_type("App.Position", [], Position, () => [[["Item1", int32_type], ["Item2", int32_type]]]);
}

export class Msg extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Move", "Restart"];
    }
}

export function Msg$reflection() {
    return union_type("App.Msg", [], Msg, () => [[["Item", Position$reflection()]], []]);
}

export function State_init() {
    return new Model(newBoard_1(3, 3), new Player(0), new GameState(1));
}

export function State_nextPlayer(_arg1) {
    if (_arg1.tag === 1) {
        return new Player(0);
    }
    else {
        return new Player(1);
    }
}

export function State_nextMove(_arg1, model) {
    const y = _arg1.fields[1] | 0;
    const x = _arg1.fields[0] | 0;
    if (canUpdateBoard(x, y, model.board)) {
        const newBoard = updateBoard(x, y, new Cell(1, model.currentPlayer), model.board);
        const nextPlayer = State_nextPlayer(model.currentPlayer);
        const newGameState = getGameState(newBoard);
        return new Model(newBoard, nextPlayer, newGameState);
    }
    else {
        return model;
    }
}

export function State_update(msg, model) {
    if (msg.tag === 1) {
        return State_init();
    }
    else {
        const position = msg.fields[0];
        return State_nextMove(position, model);
    }
}

export function View_player(src, className) {
    return react.createElement("img", {
        className: className,
        src: src,
    });
}

export const View_playerX = (className) => View_player(playerXImage, className);

export const View_playerO = (className) => View_player(playerOImage, className);

export const View_playerXM = View_playerX("PlayerX");

export const View_playerOM = View_playerO("PlayerO");

export const View_playerXS = View_playerX("PlayerX PlayerX--Small");

export const View_playerOS = View_playerO("PlayerO PlayerO--Small");

export function View_dispatchCellValue(_arg1) {
    if (_arg1.tag === 1) {
        if (_arg1.fields[0].tag === 1) {
            return View_playerOM;
        }
        else {
            return View_playerXM;
        }
    }
    else {
        return react.createElement("span", {
            className: "NoPlayer",
        });
    }
}

export function View_cell(onMove, position, cell$0027) {
    return react.createElement("button", {
        className: "Cell",
        onClick: (_arg1) => {
            onMove(position);
        },
        key: toString(position),
    }, View_dispatchCellValue(cell$0027));
}

export function View_row(onMove, x, row$0027) {
    return react.createElement("div", {
        className: "Board__Row",
        key: int32ToString(x),
    }, ...mapIndexed((y, cell$0027) => View_cell(onMove, new Position(0, x, y), cell$0027), row$0027));
}

export function View_board(onMove, board$0027) {
    return react.createElement("div", {
        className: "Board",
    }, ...mapIndexed((x, row$0027) => View_row(onMove, x, row$0027), board$0027));
}

export function View_currentPlayer(currentPlayer) {
    return react.createElement("div", {
        className: "CurrentPlayer",
    }, react.createElement("span", {
        className: "CurrentPlayer__Text",
    }, ...ofSeq(delay(() => append(singleton("Player: "), delay(() => singleton((currentPlayer.tag === 1) ? View_playerOS : View_playerXS)))))));
}

export function View_game(onMove, board$0027, currentPlayer$0027) {
    return react.createElement("div", {}, View_board(onMove, board$0027), View_currentPlayer(currentPlayer$0027));
}

export function View_gameOverPlayer(player) {
    return react.createElement("span", {}, ...ofSeq(delay(() => append(singleton("Player"), delay(() => append(singleton((player.tag === 1) ? View_playerOS : View_playerXS), delay(() => singleton("wins!"))))))));
}

export function View_gameOver(onRestart, gameOver$0027) {
    return react.createElement("div", {
        className: "GameOver",
    }, react.createElement("img", {
        className: "GameOver__Image",
        onClick: (_arg1) => {
            onRestart();
        },
        src: restartImage,
        alt: "Restart",
    }), react.createElement("p", {
        className: "GameOver__Text",
    }, ...ofSeq(delay(() => {
        if (gameOver$0027.tag === 0) {
            const player = gameOver$0027.fields[0];
            return singleton(View_gameOverPlayer(player));
        }
        else {
            return singleton("It\u0027s a draw");
        }
    }))));
}

export function View_root(model, dispatch) {
    return react.createElement("div", {
        className: "App",
    }, ...ofSeq(delay(() => {
        const matchValue = model.gameState;
        if (matchValue.tag === 0) {
            const gameOver$0027 = matchValue.fields[0];
            return singleton(View_gameOver(() => {
                dispatch(new Msg(1));
            }, gameOver$0027));
        }
        else {
            return singleton(View_game((arg) => {
                dispatch(new Msg(0, arg));
            }, model.board, model.currentPlayer));
        }
    })));
}

ProgramModule_run(Program_withReactBatched("root", ProgramModule_mkSimple(State_init, State_update, View_root)));

