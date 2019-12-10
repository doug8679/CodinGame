using System;
using System.Collections.Generic;
using System.Linq;

public class Program {
//from collections import namedtuple
//from collections import deque

// A dictionnary linking directions to their vectors
static Dictionary<string, (int x, int y)> moves = new Dictionary<string, (int x, int y)>{
    {"LEFT", (-1, 0)},
    {"UP", (0, -1)},
    {"RIGHT", (1, 0)},
    {"DOWN", (0, 1)}
};
// A dictionnary linking directions to their opposite
static Dictionary<string, string> oppositeMove = new Dictionary<string, string>{
    {"LEFT", "RIGHT"},
    {"UP", "DOWN"},
    {"RIGHT", "LEFT"},
    {"DOWN", "UP"}
};

// Here is a function allowing me to add two vectors
static (int x, int y) addPositions((int x, int y) v1, (int x, int y) v2) {
    return (v1.x + v2.x, v1.y + v2.y);
}

// Now we're going to create our tile object
class Tile {
    string[] availableMoves;
    (int x, int y) pos;
    // This function allows us to pass parameters when creating the object
    public Tile((int x, int y) pos, string code){
        this.pos = pos; // the coordinates of the tile
        // the directions that you can take from this tile
        availableMoves = parseCode(code);
    }

    // this function allows us to use the '0101' code of the tile and turn it into a list of directions
    string[] parseCode(string code){
        List<string> moves = new List<string>();
        if (code[0] == '1')
            moves.Add("UP");
        if (code[1] == '1')
            moves.Add("RIGHT");
        if (code[2] == '1')
            moves.Add("DOWN");
        if (code[3] == '1')
            moves.Add("LEFT");
        return moves.ToArray();
    }
    
    // Returns the tile in direction direction
    public Tile getTileInDirection(Dictionary<(int x, int y), Tile> board, string direction){
        (int x, int y) vect = moves[direction];
        var pos = addPositions(this.pos, vect);
        return board[pos];
    }

    // Returns all the direction you can take from a tile
    public List<string> getAccessibleDirections(Dictionary<(int x, int y), Tile> board){
        var steps = getPossibleSteps(board);
        return steps.Select(s => s.Item1).ToList();
    }

    // Returns all the accessible tiles from this tile in one move
    (string, Tile)[] getPossibleSteps(Dictionary<(int x, int y), Tile> board){
        List<(string, Tile)> direc = new List<(string, Tile)>();
        foreach(var move in availableMoves){
            var vect = moves[move];
            var dest = addPositions(pos, vect);
            
            if (board.ContainsKey(dest) && board[dest].availableMoves.Contains(oppositeMove[move]))
                direc.Add((move, board[dest]));
        }
        
        return direc.ToArray();
    }

    //# Returns the path from this tile to the tile you want
    public string[] findPath(Dictionary<(int x, int y), Tile> board, (int x, int y) pos) {
        var currentTile = this;
        var visited = new List<Tile>{currentTile};
        var toVisit = new Queue<(Tile, List<string>)>();
        toVisit.Enqueue((currentTile, new List<string>()));
        while (toVisit.Any()) {
            var data = toVisit.Dequeue();
            currentTile = data.Item1;
            var path = data.Item2;
            if (currentTile.pos == pos)
                return path.ToArray();
            else {
                foreach (var step in currentTile.getPossibleSteps(board))
                {
                    if(!visited.Contains(step.Item2)) {
                        var newPath = new List<string>(path);
                        newPath.Add(step.Item1);
                        toVisit.Enqueue((step.Item2, newPath));
                        visited.Add(step.Item2);
                    }
                }
            }
        }
        return new string[0];
    }
}

public static void Main() 
{
    int boardWidth = 7, boardHeight = 7;
    
    // Here are my global variables, these keep their values between turns
    int column = 0;
    int row = 0;
    bool toggle = true;
    int moveType = 0;
    
    // I don't know if i'm player one or not yet
    int player = -1;

    // game loop
    while(true) {
        // Here are my local variables, these are reset at each turn
        var playerinfos = new List<(int num_player_cards, int player_x, int player_y, string player_tile)>();
        var quests = new List<string>();
        var items = new List<(string item_name, int item_x, int item_y)>();
        
        Dictionary<(int x, int y), Tile> board = new Dictionary<(int x, int y), Tile>();  // The board is a dictionnary linking the position of a tile to the tile object
        
        // Here we get the type of turn : 0 for push and 1 for move
        int turn_type = int.Parse(Console.ReadLine());
        
        // Here we create our representation of the board
        for (int y=0; y<boardHeight; y++) {
            int x = 0;
            foreach(var tile in Console.ReadLine().Split()) {
                var temp = new Tile((x, y), tile);
                board[(x, y)] = new Tile((x, y), tile);
                x += 1;
            }
        }
        
        // Here we get the player infos
        for (int i=0; i<2; i++) {
            //num_player_cards, player_x, player_y, player_tile = input().split()
            var pieces = Console.ReadLine().Split();
            var num_player_cards = int.Parse(pieces[0]);
            var player_x = int.Parse(pieces[1]);
            var player_y = int.Parse(pieces[2]);
            playerinfos.Add((num_player_cards, player_x, player_y, pieces[3]));
        }
        
        //# Here we get the position of our items (and ignore the ones of the other player)
        var num_items = int.Parse(Console.ReadLine());
        for (int i=0; i<num_items; i++) {
            //item_name, item_x, item_y, item_player_id = input().split()
            var pieces = Console.ReadLine().Split();
            string item_name = pieces[0];
            int item_x = int.Parse(pieces[1]);
            int item_y = int.Parse(pieces[2]);
            int item_player_id = int.Parse(pieces[3]);
            if(item_player_id == 0)
                items.Add((item_name, item_x, item_y));
        }
        
        //# Here we get le list of the items we want (and ignore the ones of the other player)
        int num_quests = int.Parse(Console.ReadLine());
        for (int i=0; i<num_quests; i++) {
            //quest_item_name, quest_player_id = input().split()
            string[] pieces = Console.ReadLine().Split();
            int quest_player_id = int.Parse(pieces[1]);
            if (quest_player_id == 0)
                quests.Add(pieces[0]);
        }
        
        var myInfos = playerinfos[0];
        var myHeroPos = (x: myInfos.player_x, y: myInfos.player_y);
        var myHeroTile = board[myHeroPos];
        var goalPos = (x:0,y:0);
        
        //# Now we detect if we are player one or two (just to avoid having boring draws against ourself)
        if (player == -1) {
            if(myHeroPos.x == 0){
                player = 0;
            } else {
                player = 1;
            }
        };
        toggle = player == 0;  // to avoid boring draws we start our push phase differently
        
        //# We select the item we want to search
        foreach (var i in items) {
            if(i.item_name == quests[0]) {
                goalPos = (x: i.item_x, y: i.item_y);
            }
            
            //# If it's a push turn
            if(turn_type == 0) {
                //# We will alternate between vertical and horizontal push to shuffle the grid
                //# and change the line or the column we want to push at each push
                if(toggle){
                    Console.WriteLine($"PUSH {column} RIGHT");
                    column = (column + 1) % boardHeight;
                } else {
                    Console.WriteLine($"PUSH {row} DOWN");
                    row = (row + 1) % boardHeight;
                }
                toggle = !toggle;
            } else { //# If it's a move turn
                //# default value: we do nothing
                string action = "PASS";
                
                //# if we can go somewhere select one of the available directions
                if (myHeroTile.getAccessibleDirections(board).Count > 0)
                    action = "MOVE " + myHeroTile.getAccessibleDirections(board)[moveType % myHeroTile.getAccessibleDirections(board).Count];
                    
                //# if we can go to our goal item go for it (but we don't want to be too strong so we don't go for more that 2 tiles at once)
                var path = myHeroTile.findPath(board,goalPos);
                if (path.Any())
                    action = "MOVE " + string.Join(" ", path);
                    
                //# output the move we chose
                Console.WriteLine(action);
                
                //# Change the index of the next direction we'll chose if we can go somewhere but not get item
                moveType = (moveType+1) % moves.Count;
            }
        }
    }
}
}