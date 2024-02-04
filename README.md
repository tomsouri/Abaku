# Abaku documentation

**Brief entry (annotation)**
A library prepared for applications forming a virtual environment for Abaku game (https://abaku.org/). It allows you to manage the game plan, enter moves, evaluate moves, determine the validity of moves, recognize examples, and more.
Support for a smart agent. A function that tells the agent what move to play in a given situation.

**Chosen algorithm**
More complex algorithms occur in the library in the parts of move validation, move evaluation, and especially finding the best move.
A move validation algorithm is based on a more thorough examination of the rules and a systematic determination of when a move is valid and when it is not. Thus, it first examines the validity of occupied positions (if there are such positions, if they are different and free and if they are adjacent to each other, etc.), and then the existence of examples by which newly placed stones follow stones laid previously.

The thrust evaluation algorithm is based on the browsing of all examples containing any of the stones laid in a given thrust, this is done through the browsing of all positions adjacent to the newly laid stones.

Finding the best thrust involves several algorithms, but the most important of them is the one for generating all possible sequences of stones that we can lay. Then everything is done by a simple browsing: for the given unoccupied position on which we want to start and the given direction in which we want to lay the stones and the given sequence we want to lay, the laying is already unambiguous (and the determination of the validity of the thrust and subsequent evaluation can take place).

The generation of all sequences of stones is done by a simple recursion.

**Discussion of the selection of the algorithm**
When searching all possible moves, it is important to first determine the validity of the thrust as simply as possible (and without any major memory requirements), as this is determined for a large number of moves. The evaluation itself then takes place only for valid moves, of which there is a very small percentage of the total number, and therefore it can be done inefficiently.

To make the determination of the validity of the move more efficient, part of it is done in advance - part of the determination of the validity of positions - in the browsing itself we then only pass through the suitable positions at which the given sequence can start. This avoids unnecessary browsing of many non-prospective options.

**Program**

- FormulaIdentifier (part of OperationManager) - for a given sequence of digits tells whether it is an example or not
- the most important method is IsFormula(IReadOnlyList<Digit>())
- for its operation it uses FactorFormulaIdentifier, which
- Board (part of BoardManager) - provides the basic work with the game surface
- Validator - for a specific move on a specific Board tells whether the move is valid
- Evaluator - finds the score, which we get by playing a specific move on a specific Board
- Optimizer - finds the best move that we can play with given numbers on the hand on a given Board
- BoardController - provides a simple interface for all the "outside-needed" methods of the previous five parts (the only one is therefore explicitly used by the applications that make up the Abaku game environment)

**What wasn't done**

- The function for generating the best move can't generate the first move.
- Variability of the game plan - loading from a file
- The ability to add custom operations (even more-unary than unary and binary)
- window application for Abaku versus smart agent
- window application as a helper for Abaku versus opponent

-19.8.2021--
