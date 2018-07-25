namespace ConsoleApp2
{
    public abstract class Map
    {
        public abstract void CreateEnvironment();
        public abstract void InitialiseStates();
        public abstract void AddBonfires();
        public abstract void AddChests();
        public abstract void AddWalls();
        public abstract void AddPowerUps();
        public abstract void AddPowerUpRuntime(Vector2D position);
        public abstract void AddEnemies();
        public abstract void AddFirstPlayer();
        public abstract void AddSecondPlayer();
        public abstract void AddPlayer(Vector2D position, MovementScript movementScript, LookScript lookScript, WeaponScript weaponScript, AttackScript attackScript);
            
    }
}