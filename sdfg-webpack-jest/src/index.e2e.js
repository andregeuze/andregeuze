import {
    Selector
} from 'testcafe'; // first import testcafe selectors

fixture`Test add` // declare the fixture
    .page`http://localhost:3000`; // specify the start page


//then create a test and place your code there
test('My first test', async t => {
    await t
        .typeText('#left', '1')
        .takeScreenshot('myFirstTest/1.left.png')
        .typeText('#right', '2')
        .takeScreenshot('myFirstTest/2.right.png')
        .click('#add')
        .takeScreenshot('myFirstTest/3.add.png')

        // Use the assertion to check if the result is equal to the expected one
        .expect(Selector('#result-value').textContent).eql('3');
});