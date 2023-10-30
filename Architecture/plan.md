# Plan

## Files

```
Base
 ╠═ Controllers/
 ║   ║
 ║   ╠═ UserController.cs
 ║   ║
 ║   ╠═ PostController.cs
 ║   ║
 ║   ╚═ RatingController.cs
 ║
 ╠═ Models/
 ║   ║
 ║   ╠═ User.cs
 ║   ║
 ║   ╠═ Post.cs
 ║   ║
 ║   ╠═ Rating.cs
 ║   ║
 ║   ╚═ ViewModels/
 ║       ║
 ║       ╚═ UserPostRatingViewModel.cs
 ║
 ╠═ Views/
 ║   ║
 ║   ╠═ User/
 ║   ║   ║
 ║   ║   ╠═ Login.cshtml
 ║   ║   ║
 ║   ║   ╠═ Register.cshtml
 ║   ║   ║
 ║   ║   ╠═ Index.cshtml
 ║   ║   ║
 ║   ║   ╠═ Show.cshtml
 ║   ║   ║
 ║   ║   ╚═ Update_Delete.cshtml (to think about)
 ║   ║
 ║   ╚═ Post/
 ║       ║
 ║       ╠═ Index.cshtml
 ║       ║
 ║       ╠═ Show.cshtml
 ║       ║
 ║       ╚═ Create_Update_Delete.cshtml (to think about)
 ║
 ╠═
 ║
 ╚═ 
```

## Model

```js
class {

    table = 'tableName'

    //checks Id and either inserts or updates
    save(){
        if (int Id){
            UPDATE table WHERE id = Id
        }
        INSERT INTO table 
    }

    //returns element with the Id 'id'
    find(int Id){
        SELECT * FROM table WHERE id = Id
        //make object
        return object
    }

    //returns a list of elements where conditions are true
    where(string conditions){
        SELECT * FROM table WHERE conditions
        //make object
        return object
    }
}
```

## BD Setup

1. check if bd exists
2. if not, make file
3. run queries to build bd
