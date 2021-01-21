import { Union } from "./.fable/fable-library.3.1.1/Types.js";
import { union_type } from "./.fable/fable-library.3.1.1/Reflection.js";
import { ofArray, transpose, forAll, exists, tryItem, initialize, mapIndexed } from "./.fable/fable-library.3.1.1/List.js";
import { bind } from "./.fable/fable-library.3.1.1/Option.js";
import { mapCurriedArgs, equals } from "./.fable/fable-library.3.1.1/Util.js";

export class Player extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["PlayerX", "PlayerO"];
    }
}

export function Player$reflection() {
    return union_type("GameLogic.Player", [], Player, () => [[], []]);
}

export class Cell extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Empty", "Player"];
    }
}

export function Cell$reflection() {
    return union_type("GameLogic.Cell", [], Cell, () => [[], [["Item", Player$reflection()]]]);
}

export class GameOver extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Winner", "Draw"];
    }
}

export function GameOver$reflection() {
    return union_type("GameLogic.GameOver", [], GameOver, () => [[["Item", Player$reflection()]], []]);
}

export class GameState extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["GameOver", "Continue"];
    }
}

export function GameState$reflection() {
    return union_type("GameLogic.GameState", [], GameState, () => [[["Item", GameOver$reflection()]], []]);
}

export function updateBoard(x, y, cell, board) {
    return mapIndexed((x$0027, zs) => mapIndexed((y$0027, v$0027) => {
        if ((x$0027 === x) ? (y$0027 === y) : false) {
            return cell;
        }
        else {
            return v$0027;
        }
    }, zs), board);
}

export function newBoard(width, height) {
    return initialize(width, (_arg2) => initialize(height, (_arg1) => (new Cell(0))));
}

export function getField(x, y, board) {
    return bind((list_1) => tryItem(y, list_1), tryItem(x, board));
}

export function canUpdateBoard(x, y, board) {
    return equals(getField(x, y, board), new Cell(0));
}

export function checkRowsForWinner(cell, board) {
    return exists((list) => forAll((y) => equals(cell, y), list), board);
}

export function checkColsForWinner(cell, board) {
    return checkRowsForWinner(cell, transpose(board));
}

export function flip(f, a, b) {
    return f(b, a);
}

export function checkDiagonalsForWinner(cell, board) {
    const getField_1 = (b) => flip((tupledArg, board_1) => getField(tupledArg[0], tupledArg[1], board_1), board, b);
    const diag1 = ofArray([getField_1([0, 0]), getField_1([1, 1]), getField_1([2, 2])]);
    const diag2 = ofArray([getField_1([2, 0]), getField_1([1, 1]), getField_1([0, 2])]);
    return exists((list) => forAll((y_1) => equals(cell, y_1), list), ofArray([diag1, diag2]));
}

export function checkForWinner(cell, board) {
    return exists(mapCurriedArgs((check) => check(cell, board), [[0, 2]]), ofArray([(cell_1) => ((board_1) => checkRowsForWinner(cell_1, board_1)), (cell_2) => ((board_2) => checkColsForWinner(cell_2, board_2)), (cell_3) => ((board_3) => checkDiagonalsForWinner(cell_3, board_3))]));
}

export function checkForDraw(board) {
    return forAll((list) => forAll((y) => (!equals(new Cell(0), y)), list), board);
}

export function getGameState(board) {
    if (checkForWinner(new Cell(1, new Player(0)), board)) {
        return new GameState(0, new GameOver(0, new Player(0)));
    }
    else if (checkForWinner(new Cell(1, new Player(1)), board)) {
        return new GameState(0, new GameOver(0, new Player(1)));
    }
    else if (checkForDraw(board)) {
        return new GameState(0, new GameOver(1));
    }
    else {
        return new GameState(1);
    }
}

