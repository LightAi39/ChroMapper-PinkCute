using Beatmap.Base;
using Beatmap.Containers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChroMapper_PinkCute
{
    [Plugin("PinkCute")]
    public class Plugin
    {
        public GameObject _notePrefab;
        public Sprite sprite;

        private BeatmapObjectContainerCollection _beatmapObjectContainerCollection;

        [Init]
        private void Init()
        {
            sprite = LoadSprite("ChroMapper_PinkCute.Pink.png");

            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 3)
            {
                _beatmapObjectContainerCollection = UnityEngine.Object.FindObjectOfType<BeatmapObjectContainerCollection>();
                _beatmapObjectContainerCollection.ContainerSpawnedEvent += SetTexture;
            }
            else
            {
                if (_beatmapObjectContainerCollection != null)
                {
                    _beatmapObjectContainerCollection.ContainerSpawnedEvent -= SetTexture;
                }
            }
        }

        [Exit]
        private void Exit()
        {
            
        }

        private void SetTexture(BaseObject _object)
        {
            NoteContainer noteContainer = UnityEngine.Object.FindObjectOfType<NoteContainer>();

            GameObject note = noteContainer.gameObject;

            if (note.transform.Find("PinkCuteSprite") != null) return;

            GameObject spriteObject = new GameObject();
            SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();

            spriteRenderer.name = "PinkCuteSprite";
            spriteRenderer.sprite = sprite;

            spriteObject.transform.SetParent(note.transform);

            spriteObject.transform.localPosition = new Vector3(0, 0, -0.25f);
            spriteObject.transform.localScale = new Vector3(0.078f, 0.078f, 0.078f);
            spriteObject.transform.localPosition += new Vector3(-0.2f, -0.2f, 0f);
            spriteObject.transform.rotation = note.transform.rotation;
        }

        public static Sprite LoadSprite(string asset) // taken from Moizac's Extended LightIDs code because i didn't want to figure it out myself
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(asset);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);

            Texture2D texture2D = new Texture2D(256, 256);
            texture2D.LoadImage(data);

            return Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0), 100.0f);
        }
    }
}
