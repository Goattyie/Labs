package com.goattyie.lab2

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import kotlin.system.exitProcess

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }

    fun firstBtnClick(view: View)
    {
        val intent = Intent(this, SecondActivity::class.java)
        startActivity(intent)
    }

    fun secondBtnClick(view: View)
    {
        val intent = Intent(this, ThirdActivity::class.java)
        startActivity(intent)
    }

    fun thirdBtnClick(view: View)
    {
        val intent = Intent(this, FourActivity::class.java)
        startActivity(intent)
    }

    fun fourBtnClick(view: View)
    {
        moveTaskToBack(true);
        exitProcess(-1)
    }
}