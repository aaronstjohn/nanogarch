pragma solidity ^0.4.2;

//The Simulator advances the simulation  state by applying valid action requests submitted by actors
contract Simulator
{
    // enum HexagonMoves { NE, E, SE, SW,W,NW }
    // enum PentagonMoves {NE,SE,S,SW,NW}
    // enum CellShape {Hexagon,Pentagon}
    enum MoveResult{ SUCCESS,
                     ERR_ENTITY_NOT_DEFINED,
                     ERR_ENTITY_NOT_OWNED, 
                     ERR_ENTITY_NOT_IN_SRC_CELL,
                     ERR_DST_CELL_NOT_EMPTY,
                     ERR_SRC_NOT_ADJACENT_TO_DEST}
    enum SpawnResult{SUCCESS,
                    ERR_DST_CELL_NOT_EMPTY}

    enum UnitType{Tank,Infantry}
    struct Unit
    {
        uint id; 
        UnitType unitType;
        bool isDefined;   
    }
    uint _nextUnitId;
    mapping (uint => Unit) units;
    // Unit[100] units; //Max 100 units per planet very slightly cheaper 
    // mapping (uint => address) owner;
    address[100] owner;

    
    
    uint[4] inCell;
    uint[2][4] adjacentTo;
    // mapping (uint => uint ) inCell;
    // mapping (uint => uint[] ) adjacentTo;
    function Simulator()
    {
        inCell = [uint(0),1,2,3];
        adjacentTo = [[uint(1),2],[uint(0),3],[uint(0),3],[uint(1),2]];
        _nextUnitId = 0;
    }
    
    
    function isAdjacentTo(uint cell1,uint cell2) returns (bool)
    {
        uint[2] neighbors = adjacentTo[cell1];
        if(neighbors[0]==cell2||neighbors[1]==cell2)
            return true;
        return false;
    }
  
    function spawn(UnitType unitType,uint dstCell) payable returns (uint resultCode)
    {
        //Check that the dst cell is empty
        if(inCell[dstCell]!=0)
            return uint(SpawnResult.ERR_DST_CELL_NOT_EMPTY);
        
        units[_nextUnitId]= Unit(_nextUnitId,unitType,true);
       
        owner[_nextUnitId] = msg.sender;
        inCell[dstCell]=_nextUnitId;
        _nextUnitId++;
        return uint(SpawnResult.SUCCESS);
    }
    function move(uint entId,uint srcCell, uint dstCell) payable returns (uint resultCode)
    {
        Unit u = units[entId];

        //Chech that this entity exists 
        if( !units[entId].isDefined )
            return uint(MoveResult.ERR_ENTITY_NOT_DEFINED);

        //Check that this user owns this unit 
        if(owner[entId] != msg.sender)
            return uint(MoveResult.ERR_ENTITY_NOT_OWNED);

        //Check that the unit is inside the src cell 
        if(inCell[entId] != srcCell)
            return uint(MoveResult.ERR_ENTITY_NOT_IN_SRC_CELL);
        
        //Check that the src cell and dst cell are adjacent 
        if(!isAdjacentTo(srcCell,dstCell))
            return uint(MoveResult.ERR_SRC_NOT_ADJACENT_TO_DEST);
        
        //Check that the dst cell is empty
        if(inCell[dstCell]!=0)
            return uint(MoveResult.ERR_DST_CELL_NOT_EMPTY);
        
        inCell[srcCell]=0;
        inCell[dstCell]=entId;
        return uint(MoveResult.SUCCESS);
    }
}