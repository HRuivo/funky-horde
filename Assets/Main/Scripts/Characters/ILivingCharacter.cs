using System;

public interface ILivingCharacter
{
    void Heal(int amount);

    void Damage(int amount);

    void Kill();
}
