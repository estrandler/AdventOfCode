const fs = require("fs");

const { tree } = fs
  .readFileSync("7.txt")
  .toString()
  .split("\r\n")
  .reduce(
    (prev, curr) => {
      const currentArr = curr.split(" ");
      if (curr.indexOf("$ cd") === 0) {
        if (currentArr[2] === "..") {
          prev.currentPath = prev.currentPath
            .split("/")
            .slice(0, prev.currentPath.split("/").length - 1)
            .join("/");
        } else {
          const newPath = `${prev.currentPath}/${currentArr[2]}`.replace(
            "//",
            "/"
          );
          prev.currentPath = newPath;
          prev.tree[newPath] = 0;
        }
      } else if (!isNaN(parseInt(currentArr[0]))) {
        prev.tree[prev.currentPath] += parseInt(currentArr[0]);
      }

      return prev;
    },
    { currentPath: "", tree: {} }
  );

const sumTreeItems = Object.entries(tree).reduce(
  (prev, [path, count], _, arr) => {
    const children = arr.filter(
      (fullTree) => fullTree[0].indexOf(path) === 0 && fullTree[0] !== path
    );

    const sum = children.reduce((p, c) => p + c[1], count);

    if (sum < 100000) {
      prev += sum;
    }

    return prev;
  },
  0
);

console.log("a", sumTreeItems); //1581595
