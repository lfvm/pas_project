﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TempoArtist;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using TempoArtist.Beatmaps;
using TempoArtist.Objects;

namespace TempoArtist.Managers
{
    public class ManiaGameManager : MonoBehaviour
    {
        // Instance of this object
        public static ManiaGameManager instance;
        
        public GameObject OkHitEffect, goodHitEffect, perfectHitEffect, missEffect;
        
        public bool gameFinished;
        public bool useMusicTimeline;
        public bool isGameReady;

        // Reference to the GameSetup object instance
        private ManiaGameSetup GameSetup;
        
        private const float scorePerOkNote = 50f;
        private const float scorePerGoodNote = 100f;
        private const float scorePerPerfectNote = 300f;
        private const float scorePerMiss = 0f;
        
        public float ScrollSpeed { get; set; }
        public float NoteTimeOffset { get; set; } = 0;

        private int score;
        private int combo;
        private float health;
        public float OD;
        private float HPDrain;
        private int maxCombo;
        private double accuracy;
        
        private int NextObjToActivateID = 0;

        private int okHits;
        private int goodHits;
        private int perfectHits;
        private int missedHits;

        private string rank;
        
        private bool resultsCreated = false;

        public GameObject HitZone1;
        public GameObject HitZone2;
        public GameObject HitZone3;
        public GameObject HitZone4;

        public Text scoreText;
        public Text comboText;
        public Text timeText;
        public Text accuracyText;

        public bool allNotesInActive;
        private bool GamePaused;

        private Stopwatch Stopwatch { get; } = new Stopwatch();

        private void Awake()
        {
            instance = this;
            
            HitZone1 = GameObject.Find("HitZone1");
            HitZone2 = GameObject.Find("HitZone2");
            HitZone3 = GameObject.Find("HitZone3");
            HitZone4 = GameObject.Find("HitZone4");
        }

        private void Start()
        {
            GameSetup = ManiaGameSetup.instance;
            HPDrain = 5;
        }
        
        public void SetGameReady()
        {
            Stopwatch.Start();
            isGameReady = true;
        }

        private void Update()
        {
            if (!isGameReady || GamePaused)
            {
                return;
            }

            if (isGameReady)
            {
                if (!GameSetup.MusicSource.isPlaying)
                    GameSetup.MusicSource.Play();
            }
            
            if (gameFinished)
            {
                callResultsWindow();
                GameSetup.MusicSource.Stop();
            }

            if (GameSetup.notes.Count != 0 && NextObjToActivateID < GameSetup.objectActivationQueue.Count)
            {
                IterateObjectQueue();
            }
            
            if (AllNotesInActive())
                gameFinished = true;
            
            GetTimeInMs();
            
            health = handleHealth();
            rank = calculateRank(); 
            accuracy = CalculateAccuracy();
            HandleCombo(combo);
            
            timeText.text = GetTimeInMs().ToString();
            accuracyText.text = CalculateAccuracy().ToString("0.00") + "%";

            allNotesInActive = AllNotesInActive();
        }

        private void callResultsWindow()
        {
            if (!resultsCreated)
            {
                MapResult.score = score;
                MapResult.maxCombo = maxCombo;
                MapResult.okHits = okHits;
                MapResult.goodHits = goodHits;
                MapResult.perfectHits = perfectHits;
                MapResult.missedHits = missedHits;
                MapResult.rank = rank;
                MapResult.accuracy = accuracy;
                MapResult.mapId = Int32.Parse(GameSetup.Beatmap.metadata.BeatmapID);

                resultsCreated = true;
                SceneManager.LoadScene("Results");
            }
        }

        private void HandleCombo(int combo)
        {
            if (combo > maxCombo)
            {
                maxCombo = combo;
            }
        }

        private double CalculateAccuracy()
        {
            var acc = (scorePerPerfectNote * perfectHits + goodHits * scorePerGoodNote + okHits * scorePerOkNote + scorePerMiss * missedHits) /
                       ((perfectHits + goodHits + okHits + missedHits) * scorePerPerfectNote);
            return acc * 100;
        }

        private float handleHealth()
        {
            health -= HPDrain * Time.deltaTime;
            return health;
        }

        private string calculateRank()
        {
            rank = accuracy switch
            {
                var n when n >= 95 => "S",
                var n when n >= 90 => "A",
                var n when n >= 80 => "B",
                var n when n >= 70 => "C",
                100 => "X",
                _ => "D"
            };
            return rank;
        }


        private void IterateObjectQueue()
        {
            if (GetTimeInMs() >= GameSetup.objectActivationQueue[NextObjToActivateID].StartTime - Timing.ODTiming.GetODTimingForOkHit(OD))
            {
                (GameSetup.objectActivationQueue[NextObjToActivateID]).gameObject.SetActive(true);
                NextObjToActivateID++;
            }
        }

        public double GetTimeInMs()
        {
            // if (useMusicTimeline)
            // {
            //     return GameSetup.MusicSource.time * 1000;
            // }
            //
            // if (Stopwatch.Elapsed.TotalMilliseconds - startOffsetMs >= 0)
            // {
            //     useMusicTimeline = true;
            //     Stopwatch.Stop();
            //     return GameSetup.MusicSource.time * 1000;
            // }
            return Stopwatch.Elapsed.TotalMilliseconds;
        }
        
        private void NoteHit()
        {
            combo++;
            scoreText.text = score.ToString();
            comboText.text = combo.ToString();
        }

        public void OkHit()
        {
            NoteHit();
            Instantiate(OkHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
            score += (int)scorePerOkNote * combo;
            okHits++;
        }

        public void GoodHit()
        {
            NoteHit();
            Instantiate(goodHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
            score += (int)scorePerGoodNote * combo;
            goodHits++;
        }

        public void PerfectHit()
        {
            NoteHit();
            Instantiate(perfectHitEffect, new Vector3(0f, -0.7f, 0f), OkHitEffect.transform.rotation);
            score += (int)scorePerPerfectNote * combo;
            perfectHits++;
        }

        public void NoteMiss()
        {
            Instantiate(missEffect, new Vector3(0f, -0.7f, 0f), missEffect.transform.rotation);
            combo = 0;
            comboText.text = combo.ToString();
            missedHits++;
        }

        private bool AllNotesInActive()
        {
            if (GameSetup.notes.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}