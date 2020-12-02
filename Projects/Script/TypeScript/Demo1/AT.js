var fam = /** @class */ (function () {
    function fam(size) {
        this.alphas = ["senba", "sindhu", "aadithya", "sai"];
        this.dynArray = new Array();
        this.arrayAge = new Array(size);
    }
    fam.prototype.dynamicAdd = function () {
        console.log("push the elements");
        var len = 15;
        for (var i = 0; i < len; i++) {
            this.dynArray.push(i + 1);
            console.log(this.dynArray[i]);
        }
    };
    fam.prototype.dynamicRemove = function () {
        console.log("pop the elements");
        var len = this.dynArray.length;
        for (var i = 0; i < len; i++) {
            console.log(this.dynArray.pop());
        }
    };
    fam.prototype.display = function () {
        console.log(this.alphas[0]);
        console.log(this.alphas[1]);
        var i = 0;
        while (i < this.alphas.length) {
            console.log(this.alphas[i]);
            i++;
        }
    };
    fam.prototype.addValues = function () {
        for (var i = 0; i < this.arrayAge.length; i++) {
            this.arrayAge[i] = (i * 10);
            console.log(this.arrayAge[i]);
        }
    };
    return fam;
}());
var famobj = new fam(4);
famobj.display();
famobj.addValues();
famobj.dynamicAdd();
famobj.dynamicRemove();
