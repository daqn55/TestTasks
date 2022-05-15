let array = [];

function getAllElements(domElement){
    let ul = document.getElementsByTagName(domElement)

    getAllTagNames(ul)

    let tagNamesElements =(array.map(x => x.toLowerCase()));
    console.log(tagNamesElements)
}

function getAllTagNames(tagName){
    for (let i = 0; i < tagName.length; i++) {
        let element = tagName[i];
        
        array.push(element.tagName)

        if(element.hasChildNodes()){
            getAllTagNames(element.children)
        }
    }

}

