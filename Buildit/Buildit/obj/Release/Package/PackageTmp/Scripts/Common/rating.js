$(function () {
    const attrValue = +($("#star-rating").attr("data-checked"));
    const value = Math.round(attrValue);
    $("input[name=rating][value=" + value + "]").attr('checked', 'checked');

    $('#star-rating').rating(function (vote, event) {

        const id = $("#star-rating").attr("data-id");
        $.ajax({
            url: "/publication/rate",
            type: "GET",
            data: { rate: vote, id: id },
            success: function (data) {
                const newRting = Math.round(data.rating);
                const $input = $("input[name=rating][value=" + newRating + "]");
                $input
                    .prop('checked', true)
                    .siblings('input').prop('checked', false);

                $star = $(".stars a[title=" + newRting + "]");
                $star
                    .addClass('fullStar')
                    .prevAll()
                    .addClass('fullStar');

                $star.nextAll().removeClass('fullStar');
                const ratingCalculated = +data.rating;
                $("#rating-calc").text(ratingCalc.toFixed(2));
                $("#user-rating").removeClass("display-none");
                $("#user-rating span").text(vote);
            },
            error: function (err) {
                $("#rating-error").text(err.message);
            }
        });
    });
});