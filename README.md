# RPG_Project

Unity 6 (`6000.0.34f1`) ile geliştirilen bir RPG projesi.

## Gereksinimler

- **Unity Hub** ve **Unity 6000.0.34f1**
- **Git** ve **Git LFS** (büyük binary asset'ler LFS ile yönetiliyor — texture, fbx, psd vb.)

## Kurulum

```bash
# Git LFS yüklü değilse: https://git-lfs.com
git lfs install

# Repo'yu klonla
git clone https://github.com/etepeyurt/RPG_Project.git
cd RPG_Project

# LFS dosyalarını çek (klonla otomatik gelmemişse)
git lfs pull
```

Ardından Unity Hub üzerinden klasörü "Open" ile aç.

## Sahne

Ana sahne: `Assets/Scenes/SampleScene.unity`

## Yapı

```
Assets/
├── Animations/         # Karakter animasyonları (.fbx)
├── Character/          # Karakter modeli ve dokuları
├── Environments/       # Çevre asset'leri (binalar, ağaçlar, skybox)
├── Scenes/             # Unity sahneleri
└── Scripts/            # C# scriptleri
```

## Git LFS

Aşağıdaki uzantılar LFS üzerinden takip ediliyor (`.gitattributes`):
texture (`.png .jpg .tif .psd .tga .exr .bmp`), 3D model (`.fbx .obj .blend`), audio (`.wav .mp3 .ogg`), video (`.mp4 .mov`), `.unitypackage`, `.pdf`.

Yeni bir binary dosya eklerken uzantı `.gitattributes`'ta yoksa elle eklemeyi unutma.
