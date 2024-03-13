# Object Pool Manager

[![Unity 2019.1+](https://img.shields.io/badge/unity-2019.1%2B-blue.svg)](https://unity3d.com/get-unity/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/supremepanda/ObjectPoolManager/blob/master/LICENSE)
[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/furkanbaldir)

Object pooling is a software design pattern used in computer programming to manage the reuse of objects. Instead of creating and destroying objects frequently, which can be resource-intensive and impact performance, object pooling maintains a pool of pre-initialized objects. When an object is needed, it is retrieved from the pool. After its use, instead of being destroyed, the object is returned to the pool for potential reuse later.

### Why should you use this Object Pool Manager?

Because it is easy to use manager. It allows you to spawn objects using the same methods as instantiate without having to change your habits.

### Installation

First of all, this asset dependent with Zenject. If you dont want to use Zenject, you can convert it with editing some codes related with Zenject.

https://github.com/modesttree/Zenject

or if you want Asset store

https://assetstore.unity.com/packages/tools/utilities/extenject-dependency-injection-ioc-157735

then;

1. You can add git url via **Package Manager => Add package from git url**
```
https://github.com/supremepanda/ObjectPoolManager.git#upm
```

2. You can also install via git url by adding this entry in your **manifest.json**
```
"com.supremepanda.object_pool_manager": "https://github.com/supremepanda/ObjectPoolManager.git#upm"
```

### How to use?

1. You should add **ObjectPoolManagerInstaller** to your prefered context (Scene or Project Context)
2. You can inject ObjectPoolManager like this; ```[Inject] private ObjectPoolManager _objectPoolManager;```
3. You should inherit from **PoolableComponent** class for your pool objects.
4. Then you can spawn or despawn objects using this manager.
```var target = _objectPoolManager.Spawn(_prefab, Vector3.zero, Quaternion.identity);```
```_objectPoolManager.Despawn(target);```

There are other method overrides on ObjectPoolManager abstract class if you want to check.

### Important notes

- If you install ObjectPoolManager with Project Context, probably you need to reset manager caches. There is a boolean checkbox on **ObjectPoolManagerInstaller** as **ResetOnSceneChange**, you can check if you want to reset when scene changed. Or you can call **ResetPoolManager** method manually.
- If you are gettings exception related with Zenject, you can check your ZenAutoInjecter component on your PoolableComponent Gameobject. It should be set to **SceneContext**
