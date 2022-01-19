function getText(query1, query2) {
    let el = document.querySelector(query1);
    if (el === null) {
        return "";
    }
    else {
        if (query2 !== undefined) {
            el = el.querySelector(query2);
        }

        if (el === null) {
            return "";
        } else {
            return el.innerText;
        }
    }
}

function getElementByXpath(path) {
    return document.evaluate(path, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
}

function getElementsByXpath(path) {
    var result = document.evaluate(path, document, null, XPathResult.ANY_TYPE, null);
    var node, nodes = [];
    while (node = result.iterateNext())
        nodes.push(node);

    return nodes;   
}
