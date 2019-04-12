using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCell : MonoBehaviour
{
    public SpriteRenderer foodSprite; 
    public Vector2 gridPos;
    public FoodType typeOfFood = FoodType.Undefined;
    private static float foodGrow; 
    [SerializeField] ParticleSystem foodBurst;
    private float fruitScaleUpSize = 1.25f; 
    private int changeToThisFoodType = 0; 
    public enum FoodType
    {
        Undefined = -1, 
        Apple = 0, 
        Bacon = 1, 
        Brownie = 2, 
        Cheese = 3, 
        Chicken = 4, 
        Eggs = 5, 
        Honey = 6, 
        Jam =7
    } 

    //private IEnumerator DoFruitSolving()
    //{
     //   Vector3 scale = foodSprite.transform.localScale; 
      //  while (scale.x < 1.2)
    //}

    public void ChangeFruit(Board board)
    {
        int whatTypeOfFood = Random.Range(0, board.foodSprites.Length);
        typeOfFood = (FruitCell.FoodType)whatTypeOfFood;
        foodSprite.sprite = board.foodSprites[whatTypeOfFood];
    }

  

    public IEnumerator DoSolveFruit(Board board)
    {
        //gets the current size a the sprite being solved. 
        float currentScale = foodSprite.transform.localScale.x; 
        while(currentScale < fruitScaleUpSize)
        {
            //wait one frame. 
            yield return null;
            //this is how fast the sprite will grow. 
            currentScale += 0.01f;
            foodSprite.transform.localScale = Vector3.one * currentScale; 
        }
        //sets the foodBurst particle effect active 
        foodBurst.gameObject.SetActive(true);
        //sets the new sprite to be normal size
        foodSprite.transform.localScale = Vector3.one; 
        ChangeFruit(board);
        //waits for half a second before stoping the particle effect and setting it inactive
        yield return new WaitForSeconds(0.5f);
        foodBurst.Stop();
        foodBurst.gameObject.SetActive(false); 
    }

    public bool IsNeiborCell(Vector2 _gridPos)
    {
        if (gridPos.x == _gridPos.x || gridPos.y == _gridPos.y)
        {
            return true; 
        }
        return false; 
    }

    //Here is where i cheak the food that is contained in the nieboring cells. 
    public FruitCell getNeiborRight(FruitCell[,] grid)
    {
        //find the grid position to the right by incrementing x if it is less then the width of the board.
        if (gridPos.x + 1 < Board.widthOfBoard)
        {
            return grid[(int)gridPos.x + 1, (int)gridPos.y];
        }
        else
        {
            return null; 
        }
         
    }

    public FruitCell getNeiborUp(FruitCell[,] grid)
    {
        //find the grid position up by incrementing y if it is less then the hieght of the board.
        if (gridPos.y + 1 < Board.heightOfBoard)
        {
            return grid[(int)gridPos.x, (int)gridPos.y + 1];
        }
        else
        {
            return null;
        }
    }


}
