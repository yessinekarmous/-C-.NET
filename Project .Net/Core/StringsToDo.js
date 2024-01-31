function RemoveBlanks(sentence){
    var result="" ;
    for(let i=0 ; i<sentence.length ; i++){
        if(sentence[i]!==" "){
            result += sentence[i];
        }
    }
    console.log(result)
}

RemoveBlanks(" Pl ayTha tF u nkyM usi c ") 
RemoveBlanks("I can not BELIEVE it's not BUTTER")

function getDigits(word){
    var result="";
    for(let i=0 ; i<word.length ; i++){
        if(!isNaN(word[i])){
            result+= word[i];
        }
    }
    console.log(Number(result))
}
getDigits("abc8c0d1ngd0j0!8") 
getDigits("0s1a3y5w7h9a2t4?6!8?0")

function acronym(sentence) {
    var result = "";
    let words = sentence.split(' ');
    for (let i = 0; i < words.length; i++) {
            result += words[i][0].toUpperCase();
    }
    console.log(result) ;
}

acronym("there's no free lunch - gotta pay yer way");
acronym("Live from New York, it's Saturday Night!")

function countNonSpaces(sentence) {
    let j=0;
    for(let i=0 ;i< sentence.length ; i++){
        if(sentence[i]==" "){
            j++
        }
    }
    console.log(sentence.length-j)
}
countNonSpaces("Honey pie, you are driving me crazy") 
countNonSpaces("Hello world !")

function removeShorterStrings(myarr , nbr){
    newArr=[];
    let j=0
    for(let i=0 ; i<myarr.length ; i++){
        if(myarr[i].length>=nbr){
            newArr[j]=myarr[i]
            j++
        }
    }
    console.log(newArr)
}

removeShorterStrings(['Good morning', 'sunshine', 'the', 'Earth', 'says', 'hello'], 4)
removeShorterStrings(['There', 'is', 'a', 'bug', 'in', 'the', 'system'], 3)