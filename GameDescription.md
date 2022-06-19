**2D multimodal shooter game**

The idea of the game is to kill as many enemies as possible and achieve a high score!
Due to time constraints, there will only be one level.

**First modality: text recognition**
- Sensor: keyboard
- Sensory channel: visual

**Second modality: speech recognition**
- Sensor: microphone
- Sensory channel: auditory

**Modality fusion**

An example for modality fusion would be running and shooting at the same time. Since both commands will come from different modality channels, we will see modality fusion as both of them are interpreted independently. However, since it will be possible to move around and shoot at the same time, the modalities will also be used in parallel. This will make the multimodal system concurrent (see CASE properties).

**Modality fission**

Shooting and reloading at the same time is an example for modality fission. The commands are conflicting since you can't shoot and reload at the same time. A solution on how to disambiguate the conflicting input is to give reloading higher priority. This means that the player can't shoot while reloading and reloading will interrupt the shooting.

**An example for what the 2D shooter can look like**

![image](https://user-images.githubusercontent.com/79105432/174489685-917e0fc8-22bb-4c25-92c8-c8ae443e14fe.png)
Picture source: https://github.com/JumpThanawut/VoiceControlFor2DPlatformer
