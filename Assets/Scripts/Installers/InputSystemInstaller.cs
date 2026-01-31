using UnityEngine;
using Zenject;

public class InputSystemInstaller : MonoInstaller
{
    [SerializeField] InputSystem inputSystem;

    public override void InstallBindings()
    {
        Container.Bind<InputSystem>().FromComponentInNewPrefab(inputSystem).AsSingle();
    }
}