@model ShoppingCart

<h2>Shopping Cart</h2>

@if (Model.Items.Any())
{
    < table class= "table" >
        < thead >
            < tr >
                < th > Product </ th >
                < th > Quantity </ th >
                < th > Price </ th >
                < th > Total </ th >
            </ tr >
        </ thead >
        < tbody >
            @foreach(var item in Model.Items)
            {
                < tr >
                    < td > @item.Product.Name </ td >
                    < td > @item.Quantity </ td >
                    < td > @item.Product.Price </ td >
                    < td >@(item.Quantity * item.Product.Price) </ td >
                </ tr >
            }
        </ tbody >
    </ table >
}
else
{
    < p > Your shopping cart is empty.</ p >
}