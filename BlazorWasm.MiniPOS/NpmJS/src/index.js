import exportJson2Excel from '@code-zhf/export-json-2-excel';

window.exportJsonData = function (fields, data, fileName) {
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