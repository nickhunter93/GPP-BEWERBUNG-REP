namespace ConsoleApp2
{
    public class CharacterScript : Script
    {
        private GameObject _weapon;

        public CharacterScript(MovementScript movementScript, LookScript lookScript, WeaponScript weaponScript, AiScript aiScript, GameObject weapon, InvincibleScript invincibleScript, GameObject parent)
        {
            _weapon = weapon;

            parent.AddScript(this);
            _parent.AddScript(movementScript);
            _parent.AddScript(lookScript);
            _parent.AddScript(weaponScript);
            if (invincibleScript != null)
                _parent.AddScript(invincibleScript);
            if (aiScript != null)
                _parent.AddScript(aiScript);

            weapon.AddComponent(weaponScript.GetTextureComponent());
        }

        public GameObject Weapon { get => _weapon; }
    }
}