using UnityEngine;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Plataformas")]
    public GameObject[] platformPrefabs; // Array de prefabs de plataformas
    
    [Header("Configurações")]
    public int initialPlatformCount = 10; // Número inicial de plataformas
    public float platformSpacing = 5f; // Espaçamento entre plataformas
    public Vector3 startPosition = new Vector3(0, 0, 0); // Posição inicial
    
    [Header("Geração Aleatória")]
    public bool randomizeY = true; // Randomizar altura
    public float minY = -2f; // Altura mínima
    public float maxY = 2f; // Altura máxima
    public bool randomizeWidth = false; // Randomizar largura entre plataformas
    public float minSpacing = 3f; // Espaçamento mínimo
    public float maxSpacing = 7f; // Espaçamento máximo
    
    private List<GameObject> activePlatforms = new List<GameObject>();
    private Vector3 nextPosition;
    
    void Start()
    {
        nextPosition = startPosition;
        GenerateInitialPlatforms();
    }
    
    void GenerateInitialPlatforms()
    {
        for (int i = 0; i < initialPlatformCount; i++)
        {
            SpawnPlatform();
        }
    }
    
    GameObject SpawnPlatform()
    {
        // Selecionar um prefab aleatório
        int randomIndex = Random.Range(0, platformPrefabs.Length);
        GameObject platformPrefab = platformPrefabs[randomIndex];
        
        // Ajustar posição Y se necessário
        if (randomizeY)
        {
            nextPosition.y = Random.Range(minY, maxY);
        }
        
        // Instanciar a plataforma
        GameObject platform = Instantiate(platformPrefab, nextPosition, Quaternion.identity);
        activePlatforms.Add(platform);
        
        // Calcular próxima posição
        float spacing = randomizeWidth ? Random.Range(minSpacing, maxSpacing) : platformSpacing;
        nextPosition.x += spacing;
        
        return platform;
    }
    
    // Método para gerar mais plataformas (pode ser chamado externamente)
    public void GenerateMorePlatforms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnPlatform();
        }
    }
    
    // Método para limpar todas as plataformas
    public void ClearPlatforms()
    {
        foreach (GameObject platform in activePlatforms)
        {
            Destroy(platform);
        }
        activePlatforms.Clear();
        nextPosition = startPosition;
    }
    
    // Método para reiniciar a geração
    public void RestartGeneration()
    {
        ClearPlatforms();
        GenerateInitialPlatforms();
    }
}