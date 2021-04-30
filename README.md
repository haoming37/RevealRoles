ビルド手順
```
$ cd RevealRoles/RevealRoles
$ dotnet build
```

TheOtherRolesのビルドが通らない時に書き換える
```diff
diff --git a/Source Code/UsablesPatch.cs b/Source Code/UsablesPatch.cs
index 207f7c3..441f263 100644
--- a/Source Code/UsablesPatch.cs
+++ b/Source Code/UsablesPatch.cs
@@ -394,7 +394,7 @@ namespace TheOtherRoles
                         camera.transform.SetParent(__instance.transform);
                         camera.transform.position = new Vector3(surv.transform.position.x, surv.transform.position.y, 8f);
                         camera.orthographicSize = 2.35f;
-                        RenderTexture temporary = RenderTexture.GetTemporary(256, 256, 16, 0);
+                        RenderTexture temporary = RenderTexture.GetTemporary(256, 256, 16, RenderTextureFormat.ARGB32);
                         __instance.textures[i] = temporary;
```