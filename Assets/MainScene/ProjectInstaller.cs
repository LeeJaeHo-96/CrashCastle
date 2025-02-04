using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] FirebaseManager firebaseManager;

    public override void InstallBindings()
    {
        Container
            .Bind<FirebaseManager>()
            .FromInstance(firebaseManager);
    }
}
