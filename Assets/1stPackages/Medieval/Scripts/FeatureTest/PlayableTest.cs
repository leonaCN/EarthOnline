using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;


[RequireComponent(typeof(Animator))]
public class PlayableTest : MonoBehaviour
{
    public AnimationClip clip;
    public AnimationClip mixClip;
    [Range(0, 1)] public float weight;
    public RuntimeAnimatorController runtimeAnimatorController;

    PlayableGraph playableGraph;
    AnimationMixerPlayable mixerPlayable;

    // Start is called before the first frame update
    private void Start()
    {
        // Sample 1

        //playableGraph = PlayableGraph.Create("DirectClipTest");
        //playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        //var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
        //var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
        //playableOutput.SetSourcePlayable(clipPlayable);
        //playableGraph.Play();

        // Upper code can be simplified to 1 line below:
        //AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clip, out playableGraph);


        // Sample 2

        //playableGraph = PlayableGraph.Create("MixClipTest");
        //var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Final Output", GetComponent<Animator>());
        //mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        //playableOutput.SetSourcePlayable(mixerPlayable);
        //var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
        //var mixClipPlayable = AnimationClipPlayable.Create(playableGraph, mixClip);
        //playableGraph.Connect(clipPlayable, 0, mixerPlayable, 0);
        //playableGraph.Connect(mixClipPlayable, 0, mixerPlayable, 1);
        //playableGraph.Play();

        // Sample 3

        playableGraph = PlayableGraph.Create("MixClipAnimatorTest");
        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Final Output", GetComponent<Animator>());
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        playableOutput.SetSourcePlayable(mixerPlayable);
        var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
        var ctrlPlayable = AnimatorControllerPlayable.Create(playableGraph, runtimeAnimatorController);

        playableGraph.Connect(clipPlayable, 0, mixerPlayable, 0);
        playableGraph.Connect(ctrlPlayable, 0, mixerPlayable, 1);
        playableGraph.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        // Sample 1
        //if (Input.GetKey(KeyCode.P))
        //{
        //    // single clip test
        //    AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clip, out playableGraph);
        //}

        // Sample 2
        //weight = Mathf.Clamp01(weight);
        //mixerPlayable.SetInputWeight(0, 1.0f - weight);
        //mixerPlayable.SetInputWeight(1, weight);

        // Sample 3
        weight = Mathf.Clamp01(weight);
        mixerPlayable.SetInputWeight(0, 1.0f - weight);
        mixerPlayable.SetInputWeight(1, weight);
    }

    private void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
