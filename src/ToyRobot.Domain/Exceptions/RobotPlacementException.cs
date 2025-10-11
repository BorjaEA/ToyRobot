using System;

namespace ToyRobot.Domain.Exceptions
{
	/// <summary>
	/// Exception thrown when an attempt to place a robot on the board fails due to invalid conditions
	/// (e.g., a wall is present at the desired position).
	/// </summary>
	public class RobotPlacementException : InvalidOperationException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RobotPlacementException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public RobotPlacementException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a new <see cref="RobotPlacementException"/> when a wall is present at the specified position.
		/// </summary>
		/// <param name="position">The position where the robot placement failed.</param>
		/// <returns>A new <see cref="RobotPlacementException"/> instance with a standardized message.</returns>
		public static RobotPlacementException BecauseWallIsPresent(Position position)
		{
			return new RobotPlacementException($"Cannot place robot at {position} because a wall is present.");
		}

		/// <summary>
		/// Creates a new <see cref="RobotPlacementException"/> when the position is otherwise invalid.
		/// </summary>
		/// <param name="position">The position that is invalid for placement.</param>
		/// <returns>A new <see cref="RobotPlacementException"/> instance with a standardized message.</returns>
		public static RobotPlacementException BecauseInvalidPosition(Position position)
		{
			return new RobotPlacementException($"Cannot place robot at {position} because the position is invalid.");
		}
	}
}
