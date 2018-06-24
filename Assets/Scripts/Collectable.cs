using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour {
    
    protected AudioSource Audio;

    protected virtual void OnRabbitHit(HeroRabbit rabbit) {
        CollectedHide();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // if (!this.hideAnimation) {
        HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
        if (rabbit != null) {
            OnRabbitHit(rabbit);
        }
        // }
    }

    public void CollectedHide() {
        GetComponent<SpriteRenderer>().sprite = null;
        if (SoundManager.Instance.IsSoundOn() && Audio != null) {
            Audio.PlayOneShot(Audio.clip);
        }

        StartCoroutine(WaitToHide());
    }
    
    private IEnumerator WaitToHide() {
        yield return new WaitForSeconds(Audio != null ? Audio.clip.length : 0); 
        Destroy(gameObject);
    }
}