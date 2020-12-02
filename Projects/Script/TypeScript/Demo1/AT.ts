class fam
{

   alphas:string[] = ["senba","sindhu","aadithya","sai"];

   arrayAge: number[];

   dynArray: number[] = new Array();

   tuples = [1, "senba"];

   constructor(size:number)
   {
       this.arrayAge = new Array(size);
   }

   dynamicAdd()
   {
       console.log("push the elements");
      var len: number = 15;
      for(var i=0; i<len; i++)
      {
          this.dynArray.push(i+1);
          console.log(this.dynArray[i]);
      }       
   }

   dynamicRemove()
   {
       console.log("pop the elements");
       var len: number=this.dynArray.length;
       for(var i=0; i< len; i++)
       {
           console.log(this.dynArray.pop());
       }
   }

   display()
   {
    console.log(this.alphas[0]);
    console.log(this.alphas[1]);

       let i=0;
       while(i < this.alphas.length)
       {
           console.log(this.alphas[i]);
           i++;
       }
   }

   addValues()
   {
       for(var i=0; i<this.arrayAge.length; i++)
       {
           this.arrayAge[i]= (i *10);
           console.log(this.arrayAge[i]);
       }
   }


}

var famobj=new fam(4);
famobj.display();
famobj.addValues();
famobj.dynamicAdd();
famobj.dynamicRemove();

