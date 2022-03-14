# **Tempo Artist**
# *Video juego para Percussive Arts Society*

## Documento del diseño de juego

---


##
## Indice

---

1. [Index](#index)
2. [Game Design](#game-design)
    1. [Summary](#summary)
    2. [Gameplay](#gameplay)
    3. [Mindset](#mindset)
3. [Technical](#technical)
    1. [Screens](#screens)
    2. [Controls](#controls)
    3. [Mechanics](#mechanics)
4. [Level Design](#level-design)
    1. [Themes](#themes)
        1. Ambience
        2. Objects
            1. Ambient
            2. Interactive
        3. Challenges
    2. [Game Flow](#game-flow)
5. [Development](#development)
    1. [Abstract Classes](#abstract-classes--components)
    2. [Derived Classes](#derived-classes--component-compositions)
6. [Graphics](#graphics)
    1. [Style Attributes](#style-attributes)
    2. [Graphics Needed](#graphics-needed)
7. [Sounds/Music](#soundsmusic)
    1. [Style Attributes](#style-attributes-1)
    2. [Sounds Needed](#sounds-needed)
    3. [Music Needed](#music-needed)
8. [Schedule](#schedule)

## Diseño del juego

---

### **Resumen**

Un videojuego simple y retador, el cual ayude a los usuarios a mejorar su ritmo asi como su sincronización, con fin de aprender a identificar los elementos básicos de una partitura a través de distintos niveles y temporizadores.

El cliente accedera al juego mediante un sitio web, donde tambien podra consultar sus estadisticas.


### **Gameplay**

El usuario debera de presionar las teclas correspondientes, al mismo tiempo que van apareciendo notas musicales en la pantalla. Mientras mas preciso sean los clicks, mejor score tendra el jugador. En caso de que el usuario falle en presionar las teclas en el momento correcto, se iran restando puntos.

 <img width="607" alt="Captura de Pantalla 2022-03-09 a la(s) 13 47 10" src="https://user-images.githubusercontent.com/57450093/157521932-d8ed2dcd-26c9-42d0-961c-06e978c9db43.png">


### **Mentality**

The game aims to feel the player motivated to learn about musical rhythms, as well as improve their coordination and cognitive skills.
It is a challenging game where the user must strive to improve and reach the next levels.

## Aspectos tecnicos

---

### **Screems**


1. Main Menu
    1. Play
    2. Game Options
2. Difficutlty selection
3. Game


### **Controls**

The player presses the notes with the keys assigned to the rhythm of the level.

## Game design 

---

The levels will be generated with an algorithm that generates "Random" note patterns. These levels after being generated with stored in the game
so that they are the same for all players.

## _Development_

---

### **Abstract Classes / Components**

1. Note
2. Lane


### **Derived Classes / Component Compositions**

1. GameController
2. LevelGenerator
3. Note
	1. SingleNote
	2. DoubleNote
	3. TripleNote

## _Graphics_

Neon style graphics.

### **Style Attributes**

What kinds of colors will you be using? Do you have a limited palette to work with? A post-processed HSV map/image? Consistency is key for immersion.

What kind of graphic style are you going for? Cartoony? Pixel-y? Cute? How, specifically? Solid, thick outlines with flat hues? Non-black outlines with limited tints/shades? Emphasize smooth curvatures over sharp angles? Describe a set of general rules depicting your style here.

Well-designed feedback, both good (e.g. leveling up) and bad (e.g. being hit), are great for teaching the player how to play through trial and error, instead of scripting a lengthy tutorial. What kind of visual feedback are you going to use to let the player know they&#39;re interacting with something? That they \*can\* interact with something?

### **Graphics Needed**

1. Background
2. Notes
	1. Single Note
	2. Double Note
	3. Triple Note
3. Lanes
4. HitArea 

## _Sounds/Music_

1. Nota Correcta
2. Nota fallo
3. Musica de menu?
4. Nivel terminado
5. Nivel Perdido
6. Click botones menu

### **Style Attributes**

Again, consistency is key. Define that consistency here. What kind of instruments do you want to use in your music? Any particular tempo, key? Influences, genre? Mood?

Stylistically, what kind of sound effects are you looking for? Do you want to exaggerate actions with lengthy, cartoony sounds (e.g. mario&#39;s jump), or use just enough to let the player know something happened (e.g. mega man&#39;s landing)? Going for realism? You can use the music style as a bit of a reference too.

 Remember, auditory feedback should stand out from the music and other sound effects so the player hears it well. Volume, panning, and frequency/pitch are all important aspects to consider in both music _and_ sounds - so plan accordingly!

### **Sounds Needed**

1. Effects
    1. Soft Footsteps (dirt floor)
    2. Sharper Footsteps (stone floor)
    3. Soft Landing (low vertical velocity)
    4. Hard Landing (high vertical velocity)
    5. Glass Breaking
    6. Chest Opening
    7. Door Opening
2. Feedback
    1. Relieved &quot;Ahhhh!&quot; (health)
    2. Shocked &quot;Ooomph!&quot; (attacked)
    3. Happy chime (extra life)
    4. Sad chime (died)

_(example)_

### **Music Needed**

1. Slow-paced, nerve-racking &quot;forest&quot; track
2. Exciting &quot;castle&quot; track
3. Creepy, slow &quot;dungeon&quot; track
4. Happy ending credits track
5. Rick Astley&#39;s hit #1 single &quot;Never Gonna Give You Up&quot;

_(example)_


## _Schedule_

---

_(define the main activities and the expected dates when they should be finished. This is only a reference, and can change as the project is developed)_

1. develop base classes
    1. base entity
        1. base player
        2. base enemy
        3. base block
  2. base app state
        1. game world
        2. menu world
2. develop player and basic block classes
    1. physics / collisions
3. find some smooth controls/physics
4. develop other derived classes
    1. blocks
        1. moving
        2. falling
        3. breaking
        4. cloud
    2. enemies
        1. soldier
        2. rat
        3. etc.
5. design levels
    1. introduce motion/jumping
    2. introduce throwing
    3. mind the pacing, let the player play between lessons
6. design sounds
7. design music

_(example)_
