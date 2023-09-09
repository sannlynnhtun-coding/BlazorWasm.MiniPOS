Let’s start by adding a new folder named NpmJS to our project.

Create `NpmJS` Folder

Open a command in the NpmJS directory and initialize npm by running the following command:

```
npm init -y
```

Next, we need to install a JavaScript bundler. In this example, I will be using webpack. Install webpack and the webpack CLI as development dependencies by running the following command:

```
npm install webpack webpack-cli --save-dev
```

Next, we need to add a new folder called src, and then create a new JavaScript file named index.js under the src folder. Your structure should look something like this:

Create `src` Folder in NpmJS

Create `index.js` File in `src` Folder

Modify the scripts section of the `package.json` file to add the following build script.

```
"scripts": {
	"build": "webpack ./src/index.js --output-path ../wwwroot/js --output-filename index.bundle.js"
},
```

This build script tells webpack to use the index.js file we created in the src folder as the source file. Then it sets the output directory of the bundled file to be placed in a folder called js under the wwwroot directory of our application. Finally, we are setting the name of the bundled JavaScript file to index.bundle.js.


https://www.npmjs.com/package/@code-zhf/export-json-2-excel
To install `@code-zhf/export-json-2-excel` using npm, open your command line in the NpmJS directory and run the following command:

```
npm i @code-zhf/export-json-2-excel
```

Copy and paste script to our `index.js`


```
import exportJson2Excel from '@code-zhf/export-json-2-excel';

window.exportJsonData = function(fields, data, fileName) {
    console.log({ fields, data, fileName });
    //const fields = [
    //    { title: '产品名称', dataIndex: 'name' },
    //    { title: '剩余个数', dataIndex: 'value' },
    //];
    //const data = [
    //    { name: '产品1', value: 2 },
    //    { name: '产品2', value: 21 },
    //    { name: '产品3', value: 12 },
    //];
    /**
     * exportJson2Excel 接受三个参数
     * fields 是一个数组，格式为 [{ title, dataIndex }]
     * data 是一个数组，需要导出的数据
     * name 导出文件的名称
     */
    exportJson2Excel(fields, data, fileName);
};
```

Open your command line in the NpmJS directory and run the following command:

```
npm run build
```

Modify the csproj to add a pre-build step that will run our npm build script

```
<Target Name="PreBuild" BeforeTargets="PreBuildEvent">
    <Exec Command="npm install" WorkingDirectory="NpmJS" />
    <Exec Command="npm run build" WorkingDirectory="NpmJS" />
</Target>
```

```
<Target Name="BeforeBuild" BeforeTargets="Build">
	<Exec Command="npm install" WorkingDirectory="NpmJS" />
	<Exec Command="npm run build" WorkingDirectory="NpmJS" />
</Target>
```

After the npm build script has been run and the index.bundle.js file has been generated, we need to update the wwwroot/index.html file to include our newly generated index.bundle.js file just after the blazor.webassembly.js file.
```
<script src="js/index.bundle.js"></script>
```
