using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
public enum TurnType {
        PUSH = 0,
        MOVE = 1
    }
public enum PlayerId{
    NO_ONE = -1,
    ME=0,
    OPPONENT=1
}

[Flags]
public enum PathEnum {
    UP = 0x01,
    RIGHT = 0x02,
    DOWN = 0x04,
    LEFT = 0x08
}

public class QuestItem {
    public string Name {get;set;}
    public int ItemX {get;set;}
    public int ItemY {get;set;}
    public PlayerId Owner {get;set;}

    public bool IsMine() {
        return Owner == PlayerId.ME;
    }

    public PlayerId WhoseTileAmIOn() {
        var result = PlayerId.NO_ONE;
        if (ItemX == ItemY) {
            switch (ItemX) {
                case -1:
                    result = PlayerId.ME;
                    break;
                case -2:
                    result = PlayerId.OPPONENT;
                    break;
            }
        }
        return result;
    }
}
/**
 * Help the Christmas elves fetch presents in a magical labyrinth!
 **/
class Player
{
    static int myCards, myX, myY, targetX, targetY;
    static GameTile _myTile;
    static GameBoard _board;
    static List<string> _myQuests = new List<string>();
    static List<QuestItem> items = new List<QuestItem>();
    static QuestItem curItem;
    static void Main(string[] args)
    {
        string[] inputs;

        // game loop
        while (true)
        {
            TurnType turnType = (TurnType)Enum.Parse(typeof(TurnType), Console.ReadLine());
            List<string[]> board = new List<string[]>();
            for (int i = 0; i < 7; i++)
            {
                board.Add(Console.ReadLine().Split(' '));
            }
            _board = new GameBoard(board);

            //for (int i = 0; i < 2; i++)
            //{
                inputs = Console.ReadLine().Split(' ');
                myCards = int.Parse(inputs[0]); // the total number of quests for a player (hidden and revealed)
                myX = int.Parse(inputs[1]);
                myY = int.Parse(inputs[2]);
                _board.MyPosition = new int[]{myX, myY};
                _myTile = new GameTile(inputs[3]);
            //}
            Console.ReadLine(); // Ignore opponents info for now

            int numItems = int.Parse(Console.ReadLine()); // the total number of items available on board and on player tiles
            for (int i = 0; i < numItems; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                string itemName = inputs[0];
                int itemX = int.Parse(inputs[1]);
                int itemY = int.Parse(inputs[2]);
                PlayerId itemPlayerId = (PlayerId)Enum.Parse(typeof(PlayerId), inputs[3]);
                items.Add(new QuestItem{Name = itemName, ItemX = itemX, ItemY = itemY, Owner = itemPlayerId});
            }
            int numQuests = int.Parse(Console.ReadLine()); // the total number of revealed quests for both players
            for (int i = 0; i < numQuests; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                string questItemName = inputs[0];
                PlayerId questPlayerId = (PlayerId)Enum.Parse(typeof(PlayerId), inputs[1]);
                if (questPlayerId == PlayerId.ME){
                    _myQuests.Add(questItemName);
                }
            }

            if (curItem == null) {
                curItem = items.FirstOrDefault(i => i.Name.Equals(_myQuests.FirstOrDefault() ?? ""));
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            string command = "PASS";
            switch (turnType){
                case TurnType.MOVE:
                    command = DetermineMoves();
                    break;
                case TurnType.PUSH:
                    command = DeterminePushPositionAndDirection();
                    break;
            }
            Console.WriteLine(command); // PUSH <id> <direction> | MOVE <direction> | PASS
        }
    }

    private static string DetermineMoves() {
        return _board.FindPath(curItem);
    }
    static bool vert = false;
    private static string DeterminePushPositionAndDirection() {
        string result = "PUSH ";
        // What do I push?  Where do I push it?
        if (vert) {
            result += $"{myX + 1} DOWN";
            vert = !vert;
        } else {
            result += $"{myY + 1} RIGHT";
            vert = !vert;
        }
        return result;
    }
}

class GameBoard {
        public GameBoard(List<string[]> board) {
            Board = new GameTile[board.Count, board[0].Length];
            for (int row=0; row<board.Count; row++) {
                for (int col=0; col < board[row].Length; col++) {
                    Board[row, col] = new GameTile(board[row][col]);
                }
            }
            for (int row=0; row<7; row++) {
                for (int col=0; col<7; col++) {
                    if (row > 0)
                        Board[row, col].Up = Board[row - 1, col];
                    if (row < 6)
                        Board[row, col].Down = Board[row + 1, col];
                    if (col > 0)
                        Board[row, col].Left = Board[row, col - 1];
                    if (col < 6)
                        Board[row, col].Right = Board[row, col + 1];
                }
            }
        }

        public int[] MyPosition{get;set;}
        public int[] Opponent{get;set;}
        public int[] MyGoal{get;set;}
        GameTile[,] Board{get;set;}

    public PathEnum NextVert(int itemY) {
        return (MyPosition[1] < itemY) ? PathEnum.DOWN : PathEnum.UP;
    }
    public PathEnum NextHorz(int itemX) {
        return (MyPosition[0] < itemX) ? PathEnum.LEFT : PathEnum.RIGHT;
    }
    public string FindPath(QuestItem curItem)
    {
        var nextVert = NextVert(curItem.ItemY);
        var nextHorz = NextHorz(curItem.ItemX);
        var totVert = curItem.ItemY - MyPosition[0];
        var totHorz = curItem.ItemX - MyPosition[1];

        var tile = Board[MyPosition[0], MyPosition[1]];
        GameTile above, below, left, right;
        above = (MyPosition[1] > 0) ? Board[MyPosition[0], MyPosition[1]-1] : Board[MyPosition[0], 6];
        below = (MyPosition[1] < 6) ? Board[MyPosition[0], MyPosition[1]+1] : Board[MyPosition[0], 0];
        left = (MyPosition[0] > 0) ? Board[MyPosition[0]-1, MyPosition[1]] : Board[6, MyPosition[1]];
        right = (MyPosition[0] < 6)? Board[MyPosition[0]+1, MyPosition[1]] : Board[0, MyPosition[1]];

        var canVert = (tile.CanUp && above.CanDown && nextVert == PathEnum.UP) || (tile.CanDown && below.CanUp && nextVert == PathEnum.DOWN);
        var canHorz = (tile.CanLeft && left.CanRight && nextHorz == PathEnum.LEFT) || (tile.CanRight && right.CanLeft && nextHorz == PathEnum.RIGHT);
        Console.Error.WriteLine($"NV: {nextVert}, NH: {nextHorz}, TV: {totVert}, TH: {totHorz}, CV: {canVert}, CH: {canHorz}");
        var result = "PASS";
        if (canVert && canHorz) {
            Console.Error.WriteLine("Testing move quantity to decide direction");
            result = $"MOVE {((totVert>totHorz) ? nextVert : nextHorz)}";
        } else if (canVert) {
            Console.Error.WriteLine("Moving vertically");
            result = $"MOVE {nextVert}";
        } else if (canHorz) {
            Console.Error.WriteLine("Moving horizontally");
            result = $"MOVE {nextHorz}";
        }
        return result;
    }

    public string FindBestPush(QuestItem curItem) {
        string result = string.Empty;
        int xMoves = curItem.ItemX - MyPosition[0];
        int yMoves = curItem.ItemY - MyPosition[1];
        var tile = Board[MyPosition[0], MyPosition[1]];
        if (xMoves < 0 && !tile.CanLeft) {
            result = $"{MyPosition[0]-1} DOWN";
        } else if (xMoves > 0 && !tile.CanRight) {
            result = "{MyPosition[0]+1} DOWN";
        } else if (yMoves < 0 && !tile.CanUp) {
            result = "{MyPosition[1]-1} RIGHT";
        } else if (yMoves > 0 && !tile.CanDown) {
            result = "{MyPosition[1]+1} RIGHT";
        }
        return result;
    }
}

class GameTile {
    public GameTile(string paths) {
        CanUp = paths[0] == '1';
        CanRight = paths[1] == '1';
        CanDown = paths[2] == '1';
        CanLeft = paths[3] == '1';
    }
    public GameTile Up, Down, Left, Right;
    public bool CanUp, CanRight, CanDown, CanLeft;
}