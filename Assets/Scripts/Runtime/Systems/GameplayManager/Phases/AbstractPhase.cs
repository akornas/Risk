﻿public abstract class AbstractPhase : IPhase
{
	public abstract int TilesLimit { get; }
	public abstract int Tokens { get; }
	public abstract bool CanEndTurn { get; }
	public abstract bool CanBeEnded { get; }
	public abstract GamePhaseType PhaseType { get; }
	public virtual void CleanUp() { }
	public abstract void NotifyWhyCanNotEndTurn();
}

public enum GamePhaseType
{
	SettingUp,
	TakingOver
}
