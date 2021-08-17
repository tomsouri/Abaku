using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTypes;

namespace BoardManaging
{
    public interface IBoard
    {
        Digit? this[Position position] { get; }

        /// <summary>
        /// Checks, whether a stone can be placed on a concrete position.
        /// </summary>
        /// <param name="position">The position to be checked.</param>
        /// <returns>Returns true for valid empty positions. Returns false for invalid or full positions.</returns>
        bool IsPositionEmpty(Position position);

        /// <summary>
        /// Adjacent means has common edge. Occupied position is a position, where a stone is already placed.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool IsAdjacentToOccupiedPosition(Position position);

        /// <summary>
        /// Since zero is a special value in the Abaku game, we can be interested in this.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool ContainsZero(Position position);

        IEnumerable<Position> GetAdjacentOccupiedPositions(Position basePosition);

        /// <summary>
        /// Is the board empty?
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();

        /// <summary>
        /// Finds the longest part of a specified row/column (specified by positions to be contained in),
        /// which is already filled (with ignoring vacancy of specified positions). 
        /// </summary>
        /// <param name="ToBeContained">Positions which has to be contained in the section.</param>
        /// <param name="ignoreVacancy">Position, whose vacancy can be ignored.</param>
        /// <returns>The starting and the ending position of the section (as value tuple).</returns>
        (Position start, Position end) GetLongestFilledSectionBounds((Position first, Position last) ToBeContained,
                                                                     Position ignoreVacancy);

        /// <summary>
        /// Finds the longest part of a specified row/column (specified by positions to be contained in),
        /// which is already filled (with ignoring vacancy of specified positions). 
        /// </summary>
        /// <param name="ignoreVacancy">Array of those Positions, which should be contained in the section,
        /// and whose vacancy can be ignored.</param>
        /// <returns>The starting and the ending position of the section (as value tuple).</returns>
        (Position start, Position end) GetLongestFilledSectionBounds(IEnumerable<Position> ignoreVacancy);

        /// <summary>
        /// Finds the longest part of a specified row/column (specified by positions to be contained in),
        /// which is already filled (with ignoring vacancy of specified positions). 
        /// </summary>
        /// <param name="ToBeContained">Positions (2-tuple) which has to be contained in the section.</param>
        /// <param name="ignoreVacancy">Array Positions, whose vacancy can be ignored.</param>
        /// <returns>The starting and the ending position of the section (as value tuple).</returns>
        (Position start, Position end) GetLongestFilledSectionBounds((Position first, Position last) ToBeContained,
                                                                     IEnumerable<Position> ignoreVacancy);

        /// <summary>
        /// Finds the longest part of a specified row/column 
        /// (specified by a position to be contained in and the direction),
        /// which is already filled (with ignoring vacancy of specified positions). 
        /// </summary>
        /// <param name="ToBeContained">Position to be contained. Its vacancy can be ignored.</param>
        /// <param name="direction">The direction specifying row/column.</param>
        /// <returns>Bounds of the section, positions start and end (as value tuple).</returns>
        (Position start, Position end) GetLongestFilledSectionBounds(Position ToBeContained, Direction direction);

        /// <summary>
        /// Get the part of a board starting and ending on specified positions.
        /// The positions are supposed to be in the same column or row.
        /// </summary>
        /// <param name="start">The first position to be contained in the section.</param>
        /// <param name="end">The last position to be contained in the section.</param>
        /// <returns>Array of nullable digits.</returns>
        IReadOnlyList<Digit> GetSectionAfterApplyingMove(Position start, Position end, Move move);




        //TODO: je-li auxiliary array null, zavolej puvodni metodu

        /// <summary>
        /// Get the part of a board starting and ending on specified positions.
        /// The positions are supposed to be in the same column or row.
        /// </summary>
        /// <param name="start">The first position to be contained in the section.</param>
        /// <param name="end">The last position to be contained in the section.</param>
        /// <returns>Array of nullable digits.</returns>
        IReadOnlyList<Digit> GetSectionAfterApplyingMove(Position start, Position end, Move move, Digit[] auxiliaryArray);

        /// <summary>
        /// Determines whether the given position is a starting position 
        /// (the position where a stone must be placed in the first move).
        /// </summary>
        /// <param name="position">The given position to check.</param>
        /// <returns>True if it is the starting position.</returns>
        bool IsStartingPosition(Position position);
    }
}
