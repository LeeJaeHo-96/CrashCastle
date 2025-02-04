using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] GameManager gameManager;
    public override void InstallBindings()
    {
        Container
            .Bind<GameManager>()
            .FromInstance(gameManager);
    }
}