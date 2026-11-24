using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

public class AlembicPlayerController : MonoBehaviour
{
	[SerializeField] private AlembicStreamPlayer alembicPlayer;
	[SerializeField] private bool playOnStart = true;
	[SerializeField] private bool loop = true;
	[SerializeField] private float speed = 1.0f;
	private float playTo = 0;
	private bool isPlaying;

	private void Awake()
	{
		if (alembicPlayer == null)
			alembicPlayer = GetComponent<AlembicStreamPlayer>();
	}

	private void Start()
	{
		if (playOnStart)
			Play();

		//Play(1.25f, 2.5f);
	}

	private void Update()
	{
		if (!isPlaying || alembicPlayer == null)
			return;

		float nextTime = alembicPlayer.CurrentTime + Time.deltaTime * speed;

		if (nextTime >= playTo)
		{
			if (loop)
			{
				alembicPlayer.CurrentTime = 0;
			}
			else
			{
				nextTime = playTo;
				isPlaying = false;
			}
		}

		alembicPlayer.CurrentTime = nextTime;
	}

	public void Play()
	{
		alembicPlayer.CurrentTime = 0;
		playTo = alembicPlayer.Duration;
		isPlaying = true;
	}
	
	public void Play(float from, float to, bool waitEndPlay = true)
	{
		if (waitEndPlay && isPlaying)
			return;
		alembicPlayer.CurrentTime = from;
		playTo = to;
		isPlaying = true;
	}

	public void Pause() => isPlaying = false;

	public void Stop()
	{
		isPlaying = false;
		if (alembicPlayer != null)
			alembicPlayer.CurrentTime = 0f;
	}
}